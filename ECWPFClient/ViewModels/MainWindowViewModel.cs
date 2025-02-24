using ECWPFClient.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECWPFClient.ViewModels
{
  internal class MainWindowViewModel : BaseViewModel
  {
    private string _Title = "EC Client";

    public string Title
    {
      get => _Title;
      set => Set(ref _Title, value);
    }
  }
}
