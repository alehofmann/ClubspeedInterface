Namespace Views
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PickRaceView
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
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
            Me.lvwRaces = New System.Windows.Forms.ListView()
            Me.cmdUp = New System.Windows.Forms.Button()
            Me.cmdDown = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            'ButtonOK
            '
            Me.ButtonOK.BackColor = System.Drawing.Color.Transparent
            Me.ButtonOK.FlatAppearance.BorderSize = 0
            Me.ButtonOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.ButtonOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.ButtonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.ButtonOK.Location = New System.Drawing.Point(455, 530)
            Me.ButtonOK.Name = "ButtonOK"
            Me.ButtonOK.Size = New System.Drawing.Size(141, 54)
            Me.ButtonOK.TabIndex = 0
            Me.ButtonOK.UseVisualStyleBackColor = False
            '
            'ButtonCancel
            '
            Me.ButtonCancel.BackColor = System.Drawing.Color.Transparent
            Me.ButtonCancel.FlatAppearance.BorderSize = 0
            Me.ButtonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.ButtonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.ButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.ButtonCancel.Location = New System.Drawing.Point(630, 530)
            Me.ButtonCancel.Name = "ButtonCancel"
            Me.ButtonCancel.Size = New System.Drawing.Size(142, 54)
            Me.ButtonCancel.TabIndex = 1
            Me.ButtonCancel.UseVisualStyleBackColor = False
            '
            'lvwRaces
            '
            Me.lvwRaces.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.lvwRaces.Font = New System.Drawing.Font("Segoe UI Semibold", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lvwRaces.FullRowSelect = True
            Me.lvwRaces.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
            Me.lvwRaces.Location = New System.Drawing.Point(39, 63)
            Me.lvwRaces.MultiSelect = False
            Me.lvwRaces.Name = "lvwRaces"
            Me.lvwRaces.Scrollable = False
            Me.lvwRaces.Size = New System.Drawing.Size(680, 445)
            Me.lvwRaces.TabIndex = 2
            Me.lvwRaces.UseCompatibleStateImageBehavior = False
            Me.lvwRaces.View = System.Windows.Forms.View.List
            '
            'cmdUp
            '
            Me.cmdUp.BackColor = System.Drawing.Color.Transparent
            Me.cmdUp.FlatAppearance.BorderSize = 0
            Me.cmdUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.cmdUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.cmdUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cmdUp.Location = New System.Drawing.Point(716, 97)
            Me.cmdUp.Name = "cmdUp"
            Me.cmdUp.Size = New System.Drawing.Size(55, 83)
            Me.cmdUp.TabIndex = 3
            Me.cmdUp.UseVisualStyleBackColor = False
            '
            'cmdDown
            '
            Me.cmdDown.BackColor = System.Drawing.Color.Transparent
            Me.cmdDown.FlatAppearance.BorderSize = 0
            Me.cmdDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
            Me.cmdDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
            Me.cmdDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cmdDown.Location = New System.Drawing.Point(716, 392)
            Me.cmdDown.Name = "cmdDown"
            Me.cmdDown.Size = New System.Drawing.Size(55, 83)
            Me.cmdDown.TabIndex = 4
            Me.cmdDown.UseVisualStyleBackColor = False
            '
            'PickRaceView
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Global.DCS.KioskV15.ClubSpeedInterface.My.Resources.Resources.PickRace
            Me.ClientSize = New System.Drawing.Size(792, 602)
            Me.Controls.Add(Me.lvwRaces)
            Me.Controls.Add(Me.ButtonCancel)
            Me.Controls.Add(Me.ButtonOK)
            Me.Controls.Add(Me.cmdUp)
            Me.Controls.Add(Me.cmdDown)
            Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Name = "PickRaceView"
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents ButtonOK As Button
    Friend WithEvents ButtonCancel As Button
        Friend WithEvents lvwRaces As ListView
        Friend WithEvents cmdUp As Button
        Friend WithEvents cmdDown As Button
    End Class
End Namespace