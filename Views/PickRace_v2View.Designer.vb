<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PickRace_v2View
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pnlRaceButtons = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblSelectRace = New System.Windows.Forms.Label()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.lblChosenRace = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'pnlRaceButtons
        '
        Me.pnlRaceButtons.BackColor = System.Drawing.Color.Transparent
        Me.pnlRaceButtons.Location = New System.Drawing.Point(1, 61)
        Me.pnlRaceButtons.Name = "pnlRaceButtons"
        Me.pnlRaceButtons.Size = New System.Drawing.Size(896, 425)
        Me.pnlRaceButtons.TabIndex = 0
        '
        'lblSelectRace
        '
        Me.lblSelectRace.AutoSize = True
        Me.lblSelectRace.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectRace.Location = New System.Drawing.Point(-4, 28)
        Me.lblSelectRace.Name = "lblSelectRace"
        Me.lblSelectRace.Size = New System.Drawing.Size(125, 30)
        Me.lblSelectRace.TabIndex = 3
        Me.lblSelectRace.Text = "Select Race:"
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.Color.Transparent
        Me.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOK.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOK.Location = New System.Drawing.Point(703, 522)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(182, 50)
        Me.cmdOK.TabIndex = 4
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.Color.Transparent
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.Location = New System.Drawing.Point(12, 522)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(182, 50)
        Me.cmdCancel.TabIndex = 5
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'lblChosenRace
        '
        Me.lblChosenRace.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChosenRace.Location = New System.Drawing.Point(12, 489)
        Me.lblChosenRace.Name = "lblChosenRace"
        Me.lblChosenRace.Size = New System.Drawing.Size(873, 30)
        Me.lblChosenRace.TabIndex = 6
        Me.lblChosenRace.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PickRace_v2View
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(897, 584)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblChosenRace)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.lblSelectRace)
        Me.Controls.Add(Me.pnlRaceButtons)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PickRace_v2View"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PickRace_v2View"
        Me.TransparencyKey = System.Drawing.Color.Maroon
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlRaceButtons As FlowLayoutPanel
    Friend WithEvents lblSelectRace As Label
    Friend WithEvents cmdOK As Button
    Friend WithEvents cmdCancel As Button
    Friend WithEvents lblChosenRace As Label
End Class
