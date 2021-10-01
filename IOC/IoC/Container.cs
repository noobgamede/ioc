using System;
using System.Collections.Generic;
using System.Reflection;

namespace IOC.IoC
{
    public class Container : IContainer, IInternalContainer
    {
        Dictionary<Type, IProvider> _providers;
        Dictionary<Type, object> _uniqueInstances;
        Dictionary<Type, MemberInfo[]> _cachedProperties;

        public Container()
        {
            _providers = new Dictionary<Type, IProvider>();
            _uniqueInstances = new Dictionary<Type, object>();
            _cachedProperties = new Dictionary<Type, MemberInfo[]>(); 
        }

        virtual public IBinder<TContractor> Bind<TContractor>() where TContractor : class
        {
            Binder<TContractor> binder = new Binder<TContractor>();
            binder.Bind(this);
            return binder;
        }

        public void BindSelf<TContractor>() where TContractor : class, new()
        {
            IBinder<TContractor> binder = Bind<TContractor>();
            binder.AsSingle<TContractor>();
        }

        public TContractor Build<TContractor>() where TContractor : class
        {
            Type contract = typeof(TContractor);
            TContractor instance = Get(contract) as TContractor;
            DesignByContract.Check.Ensure(instance!=null,"IoC.Container instance failed to be built (contractor not found - must be registered)");
            return instance;
        }

        public TContractor Inject<TContractor>(TContractor instance)
        {
            if (instance != null) InternalInject(instance);
            return instance;
        }

        public void Register<T, K>(Type type, K provider) where K : IProvider<T>
        {
            _providers[type] = provider;
        }

        public void Release<TContractor>() where TContractor : class
        {
            Type type = typeof(TContractor);
            if (_providers.ContainsKey(type)) _providers.Remove(type);
            if (_uniqueInstances.ContainsKey(type)) _uniqueInstances.Remove(type);
        }

        protected object Get(Type contract)
        {
            if(_providers.ContainsKey(contract))
            {
                IProvider provider = _providers[contract];
                if(!_uniqueInstances.ContainsKey(provider.Contract))
                {
                    return CreateDependency(provider, contract); 
                }
                else
                {
                    return _uniqueInstances[provider.Contract];                    
                }
            }
            return null;
        }

        object Get(Type contract,Type containerContract)
        {
            IProvider provider = null;
            if(_providers.TryGetValue(contract,out provider))
            {
                object instance;
                if(!_uniqueInstances.TryGetValue(provider.Contract,out instance))
                {
                    return CreateDependency(provider,containerContract); 
                }
                else
                {
                    return instance;
                }
            }
            return null;
        }

        object CreateDependency(IProvider provider,Type containerContract)
        {
            object obj = provider.Create(containerContract);
            if (provider.Single) _uniqueInstances[provider.Contract] = obj;
            InternalInject(obj);
            return obj; 
        }

        void InternalInject(object injectable)
        {
            DesignByContract.Check.Require(injectable!=null);
            Type contract = injectable.GetType();
            Type injectAttributeType = typeof(InjectAttribute);

            MemberInfo[] properties = null;
            if(!_cachedProperties.TryGetValue(contract,out properties))
            {
                properties = contract.FindMembers(MemberTypes.Property,BindingFlags.SetProperty
                |BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance, DelegateToSearchCriteria,
                injectAttributeType
                );
                _cachedProperties[contract] = properties; 
            }
            for(int i=0;i<properties.Length;++i)
            {
                InjectProperty(injectable,properties[i]as PropertyInfo,contract); 
            }
            if (injectable is IInitialize)
                (injectable as IInitialize).OnDependencyInjected();
        }

        static bool DelegateToSearchCriteria(MemberInfo objMemberInfo,Object objSearch)
        {
            return objMemberInfo.IsDefined((Type)objSearch,true); 
        }

        void InjectProperty(object injectable,PropertyInfo info,Type contract)
        {
            if (info.PropertyType == typeof(IContainer))
                info.SetValue(injectable,this,null);
            else 
            {
                object valueObject = Get(info.PropertyType,contract);
                if (valueObject != null) info.SetValue(injectable,valueObject,null); 
            }
        }

        private sealed class Binder<Contractor> : IBinder<Contractor> where Contractor : class
        {
            IInternalContainer _container;
            Type _interfaceType;

            public void Bind(IInternalContainer container)
            {
                _container = container;
                _interfaceType = typeof(Contractor);
            }

            public void AsSingle<T>() where T : Contractor, new()
            {
                _container.Register<T, StandardProvider<T>>(_interfaceType, new StandardProvider<T>());
            }

            public void AsSingle<T>(T instance) where T : class, Contractor
            {
                _container.Register<T, SelfProvider<T>>(_interfaceType, new SelfProvider<T>(instance));
            }

            public void ToFactory<T>(IProvider<T> provider) where T : class, Contractor
            {
                _container.Register<T, IProvider<T>>(_interfaceType, provider);
            }
        }
    }
}
