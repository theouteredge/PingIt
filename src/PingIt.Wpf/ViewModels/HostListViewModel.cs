using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using PingIt.Domain;
using PingIt.Domain.Respository;

namespace PingIt.Wpf.ViewModels
{
    public class HostListViewModel : ViewModelBase
    {
        private readonly IHostRepository _repository;
        public ObservableCollection<HostListItemViewModel> Items { get; set; }

        /// <summary>
        /// ctor used by the Designer, i think...
        /// </summary>
        public HostListViewModel()
        {
            Items = new ObservableCollection<HostListItemViewModel>();
        }

        public HostListViewModel(Dispatcher dispatcher, IHostRepository repository) : base(dispatcher)
        {
            _repository = repository;

            Items = new ObservableCollection<HostListItemViewModel>();
        }

        public void Initalise()
        {
            var models = _repository.GetAll().Select(host => new HostListItemViewModel(base.Dispatcher, host));
            Items = new ObservableCollection<HostListItemViewModel>(models);
        }

        public void AddHost(Host host)
        {
            host.Ping();

            Items.Add(new HostListItemViewModel(base.Dispatcher, host));
            NotifyPropertyChanged("Items");
        }
    }
}
