using Caliburn.Micro;
using Shared.ViewModels.Abstract;

namespace Shared.ViewModels
{
    public class LoginPopupViewModel : Screen, IHaveParameter
    {
        #region Fields and Properties

        public object NavigationParameter { get; set; }

        #endregion
    }
}
