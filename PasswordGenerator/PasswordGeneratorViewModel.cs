using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace PasswordGenerator
{
    public class PasswordGeneratorViewModel : BindableBase
    {
        #region Private Fields

        private const short initialPassWordLength = 12;

        private short passwordLength;
        private string? password;

        private string? windowTitleLocalisation;
        private string? passwordLengthLocalisation;
        private string? generateLocalisation;
        private string? copyLocalisation;

        #endregion

        #region Public Properties

        public short PasswordLength
        {
            get { return passwordLength; }
            set { SetProperty(ref passwordLength, value); }
        }

        public string? Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        public string? WindowTitleLocalisation
        {
            get { return windowTitleLocalisation; }
            set { SetProperty(ref windowTitleLocalisation, value); }
        }

        public string? PasswordLengthLocalisation
        {
            get { return passwordLengthLocalisation; }
            set { SetProperty(ref passwordLengthLocalisation, value); }
        }

        public string? GenerateLocalisation
        {
            get { return generateLocalisation; }
            set { SetProperty(ref generateLocalisation, value); }
        }

        public string? CopyLocalisation
        {
            get { return copyLocalisation; }
            set { SetProperty(ref copyLocalisation, value); }
        }
        #endregion

        #region Commands

        public ICommand GenerateCommand { get; private set; }
        public ICommand CopyCommand { get; private set; }

        #endregion

        #region Constructors

        public PasswordGeneratorViewModel()
        {
            GetLocalisations();

            PasswordLength = initialPassWordLength;

            GenerateCommand = new DelegateCommand(GenerateCommandHandler);
            CopyCommand = new DelegateCommand(CopyCommandHandler);
        }

        #endregion

        #region Methods

        private void GetLocalisations()
        {
            WindowTitleLocalisation = Localisations.WindowTitle;
            PasswordLengthLocalisation = Localisations.PasswordLengthLocalisation;
            GenerateLocalisation = Localisations.GenerateLocalisation;
            CopyLocalisation = Localisations.CopyLocalisation;
        }

        private void GenerateCommandHandler()
        {
            Password = GenerateSecurePassword(passwordLength);
        }

        private void CopyCommandHandler()
        {
            if (!string.IsNullOrEmpty(Password))
            {
                Clipboard.SetText(Password);
                MessageBox.Show(Localisations.PasswordCopiedLocalisation,
                                Localisations.CopiedCaptionLocalisation,
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show(Localisations.NoPassWordLocalisation,
                                Localisations.ErrorCaptionLocalisation,
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private string GenerateSecurePassword(int length)
        {
            StringBuilder password = new StringBuilder();

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] randomNumber = new byte[1];

                while (password.Length < length)
                {
                    rng.GetBytes(randomNumber);
                    char randomChar = Localisations.ValidCharacters[randomNumber[0] % Localisations.ValidCharacters.Length];
                    password.Append(randomChar);
                }
            }

            return password.ToString();
        }

        #endregion
    }
}
