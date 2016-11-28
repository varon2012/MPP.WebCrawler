using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Core;
using Core.DataDescription;

namespace WebCrowler.ViewModel
{
    internal class MainWindowViewModel : PropertyChangedNotifier
    {
        public MainWindowViewModel()
        {
            ReferenceList = new ObservableCollection<IData>(new List<IData>()
            {
                new ReferenceDescription
                {
                    Reference = "test1",
                    ReferenceList = new ObservableCollection<IData>(new List<IData>()
                    {
                        new ReferenceDescription()
                        {
                            Reference = "test2",
                            ReferenceList = new ObservableCollection<IData>(new List<IData>()
                            {
                                new ReferenceDescription()
                                {
                                    Reference = "test3",
                                    ReferenceList = new ObservableCollection<IData>(new List<IData>()
                                    {
                                        new ReferenceDescription()
                                        {
                                            Reference = "test4",
                                            ReferenceList = new ObservableCollection<IData>(new List<IData>()
                                            {
                                                new ReferenceDescription()
                                                {
                                                    Reference = "test5",
                                                    ReferenceList =  new ObservableCollection<IData>(new List<IData>()
                                                    {
                                                        new ReferenceDescription()
                                                        {
                                                            Reference = "test6",
                                                            ReferenceList = new ObservableCollection<IData>(new List<IData>()
                                                            {
                                                                new ReferenceDescription()
                                                                {
                                                                    Reference = "test7",
                                                                    ReferenceList = new ObservableCollection<IData>(new List<IData>()
                                                                    {
                                                                        new ReferenceDescription()
                                                                        {
                                                                            Reference = "test8",
                                                                            ReferenceList = null
                                                                        }
                                                                    })
                                                                }
                                                            })
                                                        }
                                                    })
                                                }
                                            })
                                        }
                                    })
                                },
                                new ReferenceDescription()
                                {
                                    Reference = "test5",
                                    ReferenceList = null
                                }
                            })
                        }
                    })
                },
                new ReferenceDescription()
                {
                    Reference = "root2",
                    ReferenceList = new ObservableCollection<IData>(new List<IData>()
                    {
                        new ReferenceDescription()
                        {
                            Reference = "testroot1",
                            ReferenceList = null
                        }
                    })
                }
            });
            ClickCommand = new Command( () => { ClickCounter++; });
            StartCrowlingCommand = new Command(ExecuteCrowling);
        }

        public ObservableCollection<IData> ReferenceList { get; private set; }

        private int clickCounter;

        public int ClickCounter
        {
            get { return clickCounter; }
            set
            {
                clickCounter = value;
                OnPropertyChanged();
            }
        }

        public ICommand ClickCommand { get; private set; }
        public ICommand StartCrowlingCommand { get; private set; }

        private async void ExecuteCrowling()
        {
            AddNode();
        }

        private void AddNode()
        {
            ReferenceList.Add(new ReferenceDescription() {Reference = "addedtest"});
        }
    }
}
