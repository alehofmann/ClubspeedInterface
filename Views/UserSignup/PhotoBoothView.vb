
Imports System.ComponentModel
Imports AForge.Video.DirectShow
Imports DCS.KioskV15.ClubSpeedInterface.Services

Namespace Views.UserSignup
    Public Class PhotoBoothView

        Implements IUserSignupDataForm

        Private WithEvents _tmrCountdown As New Windows.Forms.Timer

        'Using AForge (NuGet packages: AForge.Controls AForge.Video.DirectShow).
        Private _result As IUserSignupDataForm.ResultCodesEnum,
                _videoCapSource As VideoCaptureDevice,
                _webCams As New FilterInfoCollection(FilterCategory.VideoInputDevice),
                _webcamName As String = String.Empty,
                _webCam As FilterInfo,
                _pbxFrame As New PictureBox,
                _countdownSeconds As Integer = 3,
                _mugshot As System.Drawing.Bitmap = Nothing

#Region "Injected Properties"

        Private _configReader As IConfigReader

        Public Property ConfigReader As IConfigReader Implements IUserSignupDataForm.ConfigReader
            Get
                Return (Me._configReader)
            End Get
            Set(ByVal newReader As IConfigReader)
                Me._configReader = newReader
            End Set
        End Property

#End Region

        Private Sub UpdateStatus()

            If (Me._countdownSeconds > 0) Then
                Me.lblCountdown.Text = Me._countdownSeconds.ToString() & "..."
            ElseIf (Me._countdownSeconds = 0) Then
                Me.lblCountdown.Text = "Smile!"
            ElseIf (Me._countdownSeconds < 0) Then
                Me.lblCountdown.Text = String.Empty
            End If
            Me.lblCountdown.Invalidate()

        End Sub

        Public Function GetUserDataItem(ByVal description As String,
                                        ByVal isFirstItem As Boolean,
                                        ByVal isLastItem As Boolean,
                                        ByRef dataEntered As String,
                                        ByVal entryType As IUserSignupView.EntryType
                                        ) As IUserSignupDataForm.ResultCodesEnum Implements IUserSignupDataForm.GetUserDataItem

            Me.lblCountdown.Text = String.Empty
            Me._webcamName = Me._configReader.GetString("Webcam", "Device Name", String.Empty).Trim().ToUpper()
            Me.cmdTakePhoto.Text = Me._configReader.GetString("Dictionary", "Take Photo Caption", "Take Photo").Trim()

            If (isFirstItem) Then
                btnPrev.Text = "Cancel"
            Else
                btnPrev.Text = "Previous"
            End If

            If (isLastItem) Then
                btnNext.Text = "Finish"
            Else
                btnNext.Text = "Next"
            End If

            If (String.Empty <> dataEntered) Then
                Me._mugshot = JSONDecodeBase64ToBitmap(dataEntered)
                Me._pbxFrame.Image = Me._mugshot
            End If

            SelectCam()

            Me.ShowDialog()

            If (Me._mugshot IsNot Nothing) Then
                dataEntered = JSONEncodeBitmapToBase64(Me._mugshot, ImageFormats.if_JPG)
            End If

            Return (_result)

        End Function

        Private Sub SelectCam()

            Me._tmrCountdown.Enabled = False
            Me._tmrCountdown.Interval = 1000

            StopCaptureIfActive()

            'Check for any cameras
            Me.cmdTakePhoto.Enabled = False
            If (Me._webCams.Count = 0) Then

                Me.lblCountdown.Text = "-ENOCAM-"
                Me.lblCountdown.BackColor = Color.Red
                Me.lblCountdown.ForeColor = Color.White
                Exit Sub

            End If

            'Now that we know there are cams, select either the desired
            'one, or the first one if no desired cam specified
            If (Me._webcamName <> String.Empty) Then
                Me._webCam = Nothing
                For Each Me._webCam In Me._webCams
                    If (Me._webCam.Name.ToUpper() = Me._webcamName) Then
                        Exit For
                    End If
                Next
                If (Me._webCam Is Nothing) Then
                    Me.lblCountdown.Text = "-EBADCAM-"
                    Me.lblCountdown.BackColor = Color.Red
                    Me.lblCountdown.ForeColor = Color.White
                    Exit Sub
                End If
            Else
                Me._webCam = Me._webCams(0)
            End If

            Me.lblCountdown.BackColor = Color.Transparent
            Me.lblCountdown.ForeColor = Me.ForeColor
            Me._pbxFrame.Visible = False
            Me.VideoPlayer.Visible = True
            Me._videoCapSource = New VideoCaptureDevice(Me._webCam.MonikerString)
            Me.VideoPlayer.VideoSource = Me._videoCapSource
            Me.VideoPlayer.Start()

            Me.cmdTakePhoto.Enabled = True

        End Sub

        Private Sub cmdTakePhoto_Click(ByVal sender As Object,
                                       ByVal e As EventArgs
                                       ) Handles cmdTakePhoto.Click

            Me.VideoPlayer.Visible = True
            Me._countdownSeconds = 3
            Me.cmdTakePhoto.Enabled = False
            Me._tmrCountdown.Enabled = True
            UpdateStatus()

        End Sub

        Private Sub btnNext_Click(ByVal sender As Object,
                                  ByVal e As EventArgs
                                  ) Handles btnNext.Click

            _result = IUserSignupDataForm.ResultCodesEnum.NextPressed
            Close()

        End Sub

        Private Sub btnPrev_Click(ByVal sender As Object,
                                  ByVal e As EventArgs
                                  ) Handles btnPrev.Click

            _result = IUserSignupDataForm.ResultCodesEnum.PreviousPressed
            Close()

        End Sub

        Private Sub _tmrCountdown_Tick(ByVal sender As Object,
                                       ByVal e As EventArgs
                                       ) Handles _tmrCountdown.Tick

            Me._countdownSeconds -= 1
            UpdateStatus()
            If (Me._countdownSeconds < 0) Then
                TakeMugshot()
                Me._tmrCountdown.Enabled = False
                Me.cmdTakePhoto.Enabled = True
            End If

        End Sub

        Private Sub TakeMugshot()

            Me._mugshot = VideoPlayer.GetCurrentVideoFrame()
            Me.VideoPlayer.Visible = False
            'Add a child picbox to the video player, making it 10% smaller
            Me._pbxFrame.Parent = Me
            Me._pbxFrame.Size = Me.VideoPlayer.Size
            Me._pbxFrame.Location = Me.VideoPlayer.Location
            Me._pbxFrame.Visible = True
            Me._pbxFrame.BorderStyle = BorderStyle.FixedSingle
            Me._pbxFrame.BackColor = Color.Transparent
            Me._pbxFrame.BackgroundImageLayout = ImageLayout.Zoom
            Me._pbxFrame.BackgroundImage = Me._mugshot

        End Sub

        Private Sub StopCaptureIfActive()

            If (Me.VideoPlayer.IsRunning) Then
                Me.VideoPlayer.SignalToStop()
                Me.VideoPlayer.WaitForStop()
            End If

        End Sub

        Private Sub PhotoBoothView_Closing(ByVal sender As Object,
                                           ByVal e As CancelEventArgs
                                           ) Handles Me.Closing

            StopCaptureIfActive()

        End Sub

    End Class
End NameSpace