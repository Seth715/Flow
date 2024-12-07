using CommunityToolkit.Maui.Views;

namespace Flow.Views;

public partial class AdminCredentialPopup : Popup
{
    public string EnteredUsername { get; private set; }
    public string EnteredPassword { get; private set; }
    public AdminCredentialPopup()
	{
		InitializeComponent();
	}

    void OnSubmit(object sender, EventArgs e)
    {
        EnteredUsername = UsernameEntry.Text;
        EnteredPassword = PasswordEntry.Text;
        Close(true); // Return true if credentials are entered
    }

    void OnCancel(object sender, EventArgs e)
    {
        Close(false); // Return false if canceled
    }
}