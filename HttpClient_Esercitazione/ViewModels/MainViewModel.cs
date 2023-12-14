using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HttpClient_Esercitazione.Services;

namespace HttpClient_Esercitazione.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        #region Proprietà

        private string _response;

        public string Response
        {
            get => _response;
            set
            {
                SetProperty(ref _response, value, nameof(Response));
            }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if(SetProperty(ref _isBusy, value, nameof(IsBusy)))
                {
                    GetListUsersAsStringResultAsyncCommand.NotifyCanExecuteChanged();
                    GetListUsersAsyncCommand.NotifyCanExecuteChanged();
                    CreateNewUserAsStringResultAsyncCommand.NotifyCanExecuteChanged();
                    CreateNewUserAsyncCommand.NotifyCanExecuteChanged();
                    GetSingleUserNotFoundAsyncCommand.NotifyCanExecuteChanged();
                    GetSingleResourceAsyncCommand.NotifyCanExecuteChanged();
                    GetDelayedResponseAsyncCommand.NotifyCanExecuteChanged();
                    DeleteUserAsyncCommand.NotifyCanExecuteChanged();
                }
            }
        }

        #endregion

        #region Comandi

        public AsyncRelayCommand GetListUsersAsStringResultAsyncCommand { get; private set; }

        public AsyncRelayCommand GetListUsersAsyncCommand { get; private set; }

        public AsyncRelayCommand CreateNewUserAsStringResultAsyncCommand { get; private set; }

        public AsyncRelayCommand CreateNewUserAsyncCommand { get; private set; }

        public AsyncRelayCommand GetSingleUserNotFoundAsyncCommand { get; private set; }

        public AsyncRelayCommand GetSingleResourceAsyncCommand { get; private set; }

        public AsyncRelayCommand GetDelayedResponseAsyncCommand { get; private set; }

        public AsyncRelayCommand DeleteUserAsyncCommand { get; private set; }

        public RelayCommand CancelOperationCommand { get; private set; }

        #endregion

        private readonly IReqresService _reqresService;

        private CancellationTokenSource _cts;

        public MainViewModel(IReqresService reqresService)
        {
            _reqresService = reqresService;

            GetListUsersAsStringResultAsyncCommand = new AsyncRelayCommand(ExecuteGetListUsersAsStringResultAsyncCommand, 
                () => !IsBusy);
            GetListUsersAsyncCommand = new AsyncRelayCommand(ExecuteGetListUsersAsyncCommand, () => !IsBusy);
            CreateNewUserAsStringResultAsyncCommand = new AsyncRelayCommand(ExecuteCreateNewUserAsStringResultAsyncCommand, 
                () => !IsBusy);
            CreateNewUserAsyncCommand = new AsyncRelayCommand(ExecuteCreateNewUserAsyncCommand, () => !IsBusy);
            GetSingleUserNotFoundAsyncCommand = new AsyncRelayCommand(ExecuteGetSingleUserNotFoundAsyncCommand, 
                () => !IsBusy);
            GetSingleResourceAsyncCommand = new AsyncRelayCommand(ExecuteGetSingleResourceAsyncCommand, () => !IsBusy);
            GetDelayedResponseAsyncCommand = new AsyncRelayCommand(ExecuteGetDelayedResponseAsyncCommand, () => !IsBusy);
            DeleteUserAsyncCommand = new AsyncRelayCommand(ExecuteDeleteUserAsyncCommand, () => !IsBusy);
            CancelOperationCommand = new RelayCommand(ExecuteCancelOperationCommand);
        }

        private async Task ExecuteGetListUsersAsStringResultAsyncCommand()
        {
            IsBusy = true;

            using (_cts = new CancellationTokenSource(3000))
            {
                var result = await _reqresService.GetListUsersAsStringResultAsync(_cts.Token);
                Response = result;
            }

            IsBusy = false;
        }

        private async Task ExecuteGetListUsersAsyncCommand()
        {
            IsBusy = true;

            using (_cts = new CancellationTokenSource(3000))
            {
                var result = await _reqresService.GetSingleResourceAsync(_cts.Token);
                Response = result.Item2;
            }

            IsBusy = false;
        }

        private async Task ExecuteCreateNewUserAsStringResultAsyncCommand()
        {
            IsBusy = true;

            using (_cts = new CancellationTokenSource(3000))
            {
                var result = await _reqresService.CreateNewUserAsStringResultAsync(_cts.Token);
                Response = result;
            }

            IsBusy = false;
        }

        private async Task ExecuteCreateNewUserAsyncCommand()
        {
            IsBusy = true;

            using (_cts = new CancellationTokenSource(3000))
            {
                var result = await _reqresService.CreateNewUserAsync(_cts.Token);
                Response = result.Item2;
            }

            IsBusy = false;
        }

        private async Task ExecuteGetSingleUserNotFoundAsyncCommand()
        {
            IsBusy = true;

            using (_cts = new CancellationTokenSource(3000))
            {
                var result = await _reqresService.GetSingleUserNotFoundAsync(_cts.Token);
                Response = result;
            }

            IsBusy = false;
        }

        private async Task ExecuteGetSingleResourceAsyncCommand()
        {
            IsBusy = true;

            using (_cts = new CancellationTokenSource(3000))
            {
                var result = await _reqresService.GetSingleResourceAsync(_cts.Token);
                Response = result.Item2;
            }

            IsBusy = false;
        }

        private async Task ExecuteGetDelayedResponseAsyncCommand()
        {
            IsBusy = true;

            using (_cts = new CancellationTokenSource(3000))
            {
                var result = await _reqresService.GetDelayedResponseAsync(_cts.Token);
                Response = result;
            }

            IsBusy = false;
        }

        private async Task ExecuteDeleteUserAsyncCommand()
        {
            IsBusy = true;

            using (_cts = new CancellationTokenSource(3000))
            {
                var result = await _reqresService.DeleteUserAsync(_cts.Token);
                Response = result;
            }

            IsBusy = false;
        }

        private void ExecuteCancelOperationCommand()
        {
            if(_cts != null)
            {
                _cts.Cancel();
            }
        }
    }
}
