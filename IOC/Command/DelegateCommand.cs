using System;
namespace IOC.Command
{
    public class DelegateCommand : ICommand
    {
        public delegate void ToExecute();
        private ToExecute _lambda;
        public DelegateCommand(ToExecute lambda)
        {
            _lambda = lambda; 
        }

        public void Execute()
        {
            _lambda();
        }
    }
}
