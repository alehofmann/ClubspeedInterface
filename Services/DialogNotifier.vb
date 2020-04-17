Imports DCS.KioskV15.ClubSpeedInterface.Services

Public Class DialogNotifier

    Implements IPopupNotifier

    Private Const CONF_SECTION As String = "Notification Screen"

    Private _configReader As IConfigReader,
            _backPicture As String = "",
            _textColor As Color = Color.Black

    Public Sub New()

        'Initialize form first and foremost so all 
        'controls are created before being used.
        InitializeComponent()

    End Sub

    Public Property ConfigReader As IConfigReader

        Get
            Return (Me._configReader)
        End Get

        Set(ByVal newEngine As IConfigReader)

            'Set engine
            Me._configReader = newEngine

            'Read config, set background pic, etc...
            _backPicture = ConfigReader.GetString(CONF_SECTION, "Background Picture", String.Empty)
            _textColor = ConfigReader.GetColor(CONF_SECTION, "Text Color", "Black")

            If (IO.File.Exists(_backPicture)) Then
                Me.BackgroundImageLayout = ImageLayout.Stretch      'Make the pic fit the form
                Me.BackgroundImage = Bitmap.FromFile(_backPicture)  'Load the picture
            End If

            lblMessage.ForeColor = _textColor

        End Set

    End Property

    Public Sub Notify(text As String) Implements IPopupNotifier.Notify

        lblMessage.Text = text
        ShowDialog()

    End Sub

    Private Sub cmdOK_Click(ByVal sender As Object,
                            ByVal e As EventArgs
                           ) Handles cmdOK.Click

        DialogResult = DialogResult.OK
        Close()

    End Sub

End Class
