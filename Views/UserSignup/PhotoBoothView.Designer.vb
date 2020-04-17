Imports AForge.Controls

Namespace Views.UserSignup
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class PhotoBoothView
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.cmdTakePhoto = New System.Windows.Forms.Button()
            Me.btnPrev = New System.Windows.Forms.Button()
            Me.btnNext = New System.Windows.Forms.Button()
            Me.lblCountdown = New System.Windows.Forms.Label()
            Me.VideoPlayer = New VideoSourcePlayer()
            Me.SuspendLayout()
            '
            'cmdTakePhoto
            '
            Me.cmdTakePhoto.BackColor = System.Drawing.Color.Transparent
            Me.cmdTakePhoto.FlatAppearance.BorderSize = 0
            Me.cmdTakePhoto.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.cmdTakePhoto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.cmdTakePhoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cmdTakePhoto.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.cmdTakePhoto.Location = New System.Drawing.Point(35, 428)
            Me.cmdTakePhoto.Name = "cmdTakePhoto"
            Me.cmdTakePhoto.Size = New System.Drawing.Size(378, 58)
            Me.cmdTakePhoto.TabIndex = 0
            Me.cmdTakePhoto.UseVisualStyleBackColor = False
            '
            'btnPrev
            '
            Me.btnPrev.BackColor = System.Drawing.Color.Transparent
            Me.btnPrev.FlatAppearance.BorderSize = 0
            Me.btnPrev.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.btnPrev.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnPrev.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnPrev.Location = New System.Drawing.Point(443, 519)
            Me.btnPrev.Name = "btnPrev"
            Me.btnPrev.Size = New System.Drawing.Size(146, 62)
            Me.btnPrev.TabIndex = 2
            Me.btnPrev.UseVisualStyleBackColor = False
            '
            'btnNext
            '
            Me.btnNext.BackColor = System.Drawing.Color.Transparent
            Me.btnNext.FlatAppearance.BorderSize = 0
            Me.btnNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnNext.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnNext.Location = New System.Drawing.Point(646, 519)
            Me.btnNext.Name = "btnNext"
            Me.btnNext.Size = New System.Drawing.Size(146, 62)
            Me.btnNext.TabIndex = 3
            Me.btnNext.UseVisualStyleBackColor = False
            '
            'lblCountdown
            '
            Me.lblCountdown.BackColor = System.Drawing.Color.Transparent
            Me.lblCountdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.lblCountdown.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblCountdown.Location = New System.Drawing.Point(586, 443)
            Me.lblCountdown.Name = "lblCountdown"
            Me.lblCountdown.Size = New System.Drawing.Size(184, 33)
            Me.lblCountdown.TabIndex = 1
            Me.lblCountdown.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'VideoPlayer
            '
            Me.VideoPlayer.BorderColor = System.Drawing.Color.Transparent
            Me.VideoPlayer.Location = New System.Drawing.Point(35, 41)
            Me.VideoPlayer.Name = "VideoPlayer"
            Me.VideoPlayer.Size = New System.Drawing.Size(735, 381)
            Me.VideoPlayer.TabIndex = 4
            Me.VideoPlayer.VideoSource = Nothing
            Me.VideoPlayer.Visible = False
            '
            'PhotoBoothView
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Global.DCS.KioskV15.ClubSpeedInterface.My.Resources.Resources.TakePicture
            Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
            Me.ClientSize = New System.Drawing.Size(804, 604)
            Me.Controls.Add(Me.VideoPlayer)
            Me.Controls.Add(Me.lblCountdown)
            Me.Controls.Add(Me.btnNext)
            Me.Controls.Add(Me.btnPrev)
            Me.Controls.Add(Me.cmdTakePhoto)
            Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "PhotoBoothView"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents cmdTakePhoto As Button
        Friend WithEvents btnPrev As Button
        Friend WithEvents btnNext As Button
        Friend WithEvents lblCountdown As Label
        Friend WithEvents VideoPlayer As VideoSourcePlayer
    End Class
End Namespace