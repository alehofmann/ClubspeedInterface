<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RaceItemButton
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RaceItemButton))
        Me.lblRaceDate = New System.Windows.Forms.Label()
        Me.lblAvailSeats = New System.Windows.Forms.Label()
        Me.lblRaceName = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblRaceDate
        '
        Me.lblRaceDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRaceDate.BackColor = System.Drawing.Color.Transparent
        Me.lblRaceDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblRaceDate.Location = New System.Drawing.Point(3, 20)
        Me.lblRaceDate.Name = "lblRaceDate"
        Me.lblRaceDate.Size = New System.Drawing.Size(110, 13)
        Me.lblRaceDate.TabIndex = 1
        Me.lblRaceDate.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblAvailSeats
        '
        Me.lblAvailSeats.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAvailSeats.BackColor = System.Drawing.Color.Transparent
        Me.lblAvailSeats.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblAvailSeats.Location = New System.Drawing.Point(3, 97)
        Me.lblAvailSeats.Name = "lblAvailSeats"
        Me.lblAvailSeats.Size = New System.Drawing.Size(110, 13)
        Me.lblAvailSeats.TabIndex = 3
        Me.lblAvailSeats.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblRaceName
        '
        Me.lblRaceName.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRaceName.BackColor = System.Drawing.Color.Transparent
        Me.lblRaceName.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblRaceName.Location = New System.Drawing.Point(3, 60)
        Me.lblRaceName.Name = "lblRaceName"
        Me.lblRaceName.Size = New System.Drawing.Size(110, 13)
        Me.lblRaceName.TabIndex = 2
        Me.lblRaceName.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'RaceItemButton
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Controls.Add(Me.lblRaceDate)
        Me.Controls.Add(Me.lblRaceName)
        Me.Controls.Add(Me.lblAvailSeats)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "RaceItemButton"
        Me.Size = New System.Drawing.Size(116, 133)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblRaceDate As Label
    Friend WithEvents lblAvailSeats As Label
    Friend WithEvents lblRaceName As Label
End Class
