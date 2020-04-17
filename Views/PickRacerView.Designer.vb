

Namespace Views
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class PickRacerView
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
            Me.lstRacers = New System.Windows.Forms.ListBox()
            Me.BackButton = New System.Windows.Forms.Button()
            Me.OKButton = New System.Windows.Forms.Button()
            Me.ButtonUp = New System.Windows.Forms.Button()
            Me.ButtonDown = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            'lstRacers
            '
            Me.lstRacers.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.lstRacers.Font = New System.Drawing.Font("Segoe UI Semibold", 18.0!, System.Drawing.FontStyle.Bold)
            Me.lstRacers.FormattingEnabled = True
            Me.lstRacers.IntegralHeight = False
            Me.lstRacers.ItemHeight = 32
            Me.lstRacers.Location = New System.Drawing.Point(25, 117)
            Me.lstRacers.Name = "lstRacers"
            Me.lstRacers.Size = New System.Drawing.Size(784, 378)
            Me.lstRacers.TabIndex = 0
            '
            'BackButton
            '
            Me.BackButton.BackColor = System.Drawing.Color.Transparent
            Me.BackButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.BackButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.BackButton.FlatAppearance.BorderSize = 0
            Me.BackButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.BackButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.BackButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.BackButton.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.BackButton.Location = New System.Drawing.Point(709, 523)
            Me.BackButton.Name = "BackButton"
            Me.BackButton.Size = New System.Drawing.Size(160, 49)
            Me.BackButton.TabIndex = 4
            Me.BackButton.UseVisualStyleBackColor = False
            '
            'OKButton
            '
            Me.OKButton.BackColor = System.Drawing.Color.Transparent
            Me.OKButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.OKButton.FlatAppearance.BorderSize = 0
            Me.OKButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.OKButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.OKButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.OKButton.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OKButton.Location = New System.Drawing.Point(474, 523)
            Me.OKButton.Name = "OKButton"
            Me.OKButton.Size = New System.Drawing.Size(160, 49)
            Me.OKButton.TabIndex = 3
            Me.OKButton.UseVisualStyleBackColor = False
            '
            'ButtonUp
            '
            Me.ButtonUp.BackColor = System.Drawing.Color.Transparent
            Me.ButtonUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.ButtonUp.FlatAppearance.BorderSize = 0
            Me.ButtonUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.ButtonUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.ButtonUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.ButtonUp.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ButtonUp.Location = New System.Drawing.Point(818, 117)
            Me.ButtonUp.Name = "ButtonUp"
            Me.ButtonUp.Size = New System.Drawing.Size(51, 84)
            Me.ButtonUp.TabIndex = 1
            Me.ButtonUp.UseVisualStyleBackColor = False
            '
            'ButtonDown
            '
            Me.ButtonDown.BackColor = System.Drawing.Color.Transparent
            Me.ButtonDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
            Me.ButtonDown.FlatAppearance.BorderSize = 0
            Me.ButtonDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.ButtonDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.ButtonDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.ButtonDown.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.ButtonDown.Location = New System.Drawing.Point(818, 411)
            Me.ButtonDown.Name = "ButtonDown"
            Me.ButtonDown.Size = New System.Drawing.Size(51, 84)
            Me.ButtonDown.TabIndex = 2
            Me.ButtonDown.UseVisualStyleBackColor = False
            '
            'PickRacerView
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Global.DCS.KioskV15.ClubSpeedInterface.My.Resources.Resources.SelectRacer
            Me.ClientSize = New System.Drawing.Size(891, 597)
            Me.Controls.Add(Me.ButtonDown)
            Me.Controls.Add(Me.ButtonUp)
            Me.Controls.Add(Me.OKButton)
            Me.Controls.Add(Me.BackButton)
            Me.Controls.Add(Me.lstRacers)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "PickRacerView"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "PickRacerView"
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents lstRacers As ListBox
        Friend WithEvents BackButton As Button
        Friend WithEvents OKButton As Button
        Friend WithEvents ButtonUp As Button
        Friend WithEvents ButtonDown As Button
    End Class
End NameSpace