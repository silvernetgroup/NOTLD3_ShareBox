using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.NetworkInformation;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class MainViewModel : BindableBase
    {
       private HeIsMyHeroViewModel _heIsMyHeroModel;
       private BlogViewModel _blogModel;
       private VideosViewModel _videosModel;
       private FacebookViewModel _facebookModel;
       private BingViewModel _bingModel;
       private AlbumsViewModel _albumsModel;
        private PrivacyViewModel _privacyModel;

        private ViewModelBase _selectedItem = null;

        public MainViewModel()
        {
            _selectedItem = HeIsMyHeroModel;
            _privacyModel = new PrivacyViewModel();

        }
 
        public HeIsMyHeroViewModel HeIsMyHeroModel
        {
            get { return _heIsMyHeroModel ?? (_heIsMyHeroModel = new HeIsMyHeroViewModel()); }
        }
 
        public BlogViewModel BlogModel
        {
            get { return _blogModel ?? (_blogModel = new BlogViewModel()); }
        }
 
        public VideosViewModel VideosModel
        {
            get { return _videosModel ?? (_videosModel = new VideosViewModel()); }
        }
 
        public FacebookViewModel FacebookModel
        {
            get { return _facebookModel ?? (_facebookModel = new FacebookViewModel()); }
        }
 
        public BingViewModel BingModel
        {
            get { return _bingModel ?? (_bingModel = new BingViewModel()); }
        }
 
        public AlbumsViewModel AlbumsModel
        {
            get { return _albumsModel ?? (_albumsModel = new AlbumsViewModel()); }
        }

        public void SetViewType(ViewTypes viewType)
        {
            HeIsMyHeroModel.ViewType = viewType;
            BlogModel.ViewType = viewType;
            VideosModel.ViewType = viewType;
            FacebookModel.ViewType = viewType;
            BingModel.ViewType = viewType;
            AlbumsModel.ViewType = viewType;
        }

        public ViewModelBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                UpdateAppBar();
            }
        }

        public Visibility AppBarVisibility
        {
            get
            {
                return SelectedItem == null ? AboutVisibility : SelectedItem.AppBarVisibility;
            }
        }

        public Visibility AboutVisibility
        {

         get { return Visibility.Visible; }
        }

        public void UpdateAppBar()
        {
            OnPropertyChanged("AppBarVisibility");
            OnPropertyChanged("AboutVisibility");
        }

        /// <summary>
        /// Load ViewModel items asynchronous
        /// </summary>
        public async Task LoadDataAsync(bool forceRefresh = false)
        {
            var loadTasks = new Task[]
            { 
                HeIsMyHeroModel.LoadItemsAsync(forceRefresh),
                BlogModel.LoadItemsAsync(forceRefresh),
                VideosModel.LoadItemsAsync(forceRefresh),
                FacebookModel.LoadItemsAsync(forceRefresh),
                BingModel.LoadItemsAsync(forceRefresh),
                AlbumsModel.LoadItemsAsync(forceRefresh),
            };
            await Task.WhenAll(loadTasks);
        }

        //
        //  ViewModel command implementation
        //
        public ICommand RefreshCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    await LoadDataAsync(true);
                });
            }
        }

        public ICommand AboutCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateToPage("AboutThisAppPage");
                });
            }
        }

        public ICommand PrivacyCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateTo(_privacyModel.Url);
                });
            }
        }
    }
}
