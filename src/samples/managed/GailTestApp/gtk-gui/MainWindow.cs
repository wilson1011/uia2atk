// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.42
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------



public partial class MainWindow {
    
    private Gtk.Action XFileAction;
    
    private Gtk.Action GimmeHelpAction;
    
    private Gtk.Action AboutAction;
    
    private Gtk.Action QuitAction;
    
    private Gtk.Action SomeEditMenuAction;
    
    private Gtk.Action NewAction;
    
    private Gtk.VBox vbox1;
    
    private Gtk.MenuBar menubar1;
    
    private Gtk.HBox hbox1;
    
    private Gtk.Table table1;
    
    private Gtk.Button btnTest1;
    
    private Gtk.ComboBoxEntry cbeTest;
    
    private Gtk.ComboBox cbxTest;
    
    private Gtk.CheckButton chkTest;
    
    private Gtk.Label lblTest1;
    
    private Gtk.RadioButton radiobutton1;
    
    private Gtk.RadioButton radiobutton2;
    
    private Gtk.RadioButton radTest1;
    
    private Gtk.RadioButton radTest2;
    
    private Gtk.Entry txtEntry;
    
    private Gtk.VScrollbar vscrollbar1;
    
    private Gtk.HScrollbar hscrollbar1;
    
    private Gtk.ProgressBar progressbar1;
    
    private Gtk.Statusbar statusbar1;
    
    protected virtual void Build() {
        Stetic.Gui.Initialize(this);
        // Widget MainWindow
        Gtk.UIManager w1 = new Gtk.UIManager();
        Gtk.ActionGroup w2 = new Gtk.ActionGroup("Default");
        this.XFileAction = new Gtk.Action("XFileAction", Mono.Unix.Catalog.GetString("XFile"), null, null);
        this.XFileAction.ShortLabel = Mono.Unix.Catalog.GetString("File");
        w2.Add(this.XFileAction, null);
        this.GimmeHelpAction = new Gtk.Action("GimmeHelpAction", Mono.Unix.Catalog.GetString("GimmeHelp"), null, null);
        this.GimmeHelpAction.ShortLabel = Mono.Unix.Catalog.GetString("Help");
        w2.Add(this.GimmeHelpAction, null);
        this.AboutAction = new Gtk.Action("AboutAction", Mono.Unix.Catalog.GetString("About"), null, null);
        this.AboutAction.ShortLabel = Mono.Unix.Catalog.GetString("About");
        w2.Add(this.AboutAction, null);
        this.QuitAction = new Gtk.Action("QuitAction", Mono.Unix.Catalog.GetString("Quit"), null, null);
        this.QuitAction.ShortLabel = Mono.Unix.Catalog.GetString("Quit");
        w2.Add(this.QuitAction, null);
        this.SomeEditMenuAction = new Gtk.Action("SomeEditMenuAction", Mono.Unix.Catalog.GetString("SomeEditMenu"), null, null);
        this.SomeEditMenuAction.ShortLabel = Mono.Unix.Catalog.GetString("SomeEditMenu");
        w2.Add(this.SomeEditMenuAction, null);
        this.NewAction = new Gtk.Action("NewAction", Mono.Unix.Catalog.GetString("New"), null, null);
        this.NewAction.ShortLabel = Mono.Unix.Catalog.GetString("New");
        w2.Add(this.NewAction, null);
        w1.InsertActionGroup(w2, 0);
        this.AddAccelGroup(w1.AccelGroup);
        this.Name = "MainWindow";
        this.Title = Mono.Unix.Catalog.GetString("MainWindow");
        this.WindowPosition = ((Gtk.WindowPosition)(4));
        // Container child MainWindow.Gtk.Container+ContainerChild
        this.vbox1 = new Gtk.VBox();
        this.vbox1.Name = "vbox1";
        this.vbox1.Spacing = 6;
        // Container child vbox1.Gtk.Box+BoxChild
        w1.AddUiFromString("<ui><menubar name='menubar1'><menu action='XFileAction'><menuitem action='NewAction'/><menuitem action='QuitAction'/></menu><menu action='SomeEditMenuAction'/><menu action='GimmeHelpAction'><menuitem action='AboutAction'/></menu></menubar></ui>");
        this.menubar1 = ((Gtk.MenuBar)(w1.GetWidget("/menubar1")));
        this.menubar1.Name = "menubar1";
        this.vbox1.Add(this.menubar1);
        Gtk.Box.BoxChild w3 = ((Gtk.Box.BoxChild)(this.vbox1[this.menubar1]));
        w3.Position = 0;
        w3.Expand = false;
        w3.Fill = false;
        // Container child vbox1.Gtk.Box+BoxChild
        this.hbox1 = new Gtk.HBox();
        this.hbox1.Name = "hbox1";
        this.hbox1.Spacing = 6;
        // Container child hbox1.Gtk.Box+BoxChild
        this.table1 = new Gtk.Table(((uint)(4)), ((uint)(3)), false);
        this.table1.Name = "table1";
        this.table1.RowSpacing = ((uint)(6));
        this.table1.ColumnSpacing = ((uint)(6));
        // Container child table1.Gtk.Table+TableChild
        this.btnTest1 = new Gtk.Button();
        this.btnTest1.CanFocus = true;
        this.btnTest1.Name = "btnTest1";
        this.btnTest1.UseUnderline = true;
        this.btnTest1.Label = Mono.Unix.Catalog.GetString("hey ya");
        this.table1.Add(this.btnTest1);
        Gtk.Table.TableChild w4 = ((Gtk.Table.TableChild)(this.table1[this.btnTest1]));
        w4.LeftAttach = ((uint)(2));
        w4.RightAttach = ((uint)(3));
        w4.XOptions = ((Gtk.AttachOptions)(4));
        w4.YOptions = ((Gtk.AttachOptions)(4));
        // Container child table1.Gtk.Table+TableChild
        this.cbeTest = Gtk.ComboBoxEntry.NewText();
        this.cbeTest.AppendText(Mono.Unix.Catalog.GetString("First element"));
        this.cbeTest.AppendText(Mono.Unix.Catalog.GetString("Second element"));
        this.cbeTest.AppendText(Mono.Unix.Catalog.GetString("Third element"));
        this.cbeTest.Name = "cbeTest";
        this.table1.Add(this.cbeTest);
        Gtk.Table.TableChild w5 = ((Gtk.Table.TableChild)(this.table1[this.cbeTest]));
        w5.TopAttach = ((uint)(1));
        w5.BottomAttach = ((uint)(2));
        w5.LeftAttach = ((uint)(2));
        w5.RightAttach = ((uint)(3));
        w5.XOptions = ((Gtk.AttachOptions)(4));
        w5.YOptions = ((Gtk.AttachOptions)(4));
        // Container child table1.Gtk.Table+TableChild
        this.cbxTest = Gtk.ComboBox.NewText();
        this.cbxTest.AppendText(Mono.Unix.Catalog.GetString("FirstItem"));
        this.cbxTest.AppendText(Mono.Unix.Catalog.GetString("SecondItem"));
        this.cbxTest.AppendText(Mono.Unix.Catalog.GetString("LastItem"));
        this.cbxTest.Name = "cbxTest";
        this.table1.Add(this.cbxTest);
        Gtk.Table.TableChild w6 = ((Gtk.Table.TableChild)(this.table1[this.cbxTest]));
        w6.TopAttach = ((uint)(1));
        w6.BottomAttach = ((uint)(2));
        w6.LeftAttach = ((uint)(1));
        w6.RightAttach = ((uint)(2));
        w6.XOptions = ((Gtk.AttachOptions)(4));
        w6.YOptions = ((Gtk.AttachOptions)(4));
        // Container child table1.Gtk.Table+TableChild
        this.chkTest = new Gtk.CheckButton();
        this.chkTest.CanFocus = true;
        this.chkTest.Name = "chkTest";
        this.chkTest.Label = Mono.Unix.Catalog.GetString("checkbutton1");
        this.chkTest.DrawIndicator = true;
        this.chkTest.UseUnderline = true;
        this.table1.Add(this.chkTest);
        Gtk.Table.TableChild w7 = ((Gtk.Table.TableChild)(this.table1[this.chkTest]));
        w7.TopAttach = ((uint)(1));
        w7.BottomAttach = ((uint)(2));
        w7.XOptions = ((Gtk.AttachOptions)(4));
        w7.YOptions = ((Gtk.AttachOptions)(4));
        // Container child table1.Gtk.Table+TableChild
        this.lblTest1 = new Gtk.Label();
        this.lblTest1.Name = "lblTest1";
        this.lblTest1.LabelProp = Mono.Unix.Catalog.GetString("This is a test message\nin a label");
        this.table1.Add(this.lblTest1);
        Gtk.Table.TableChild w8 = ((Gtk.Table.TableChild)(this.table1[this.lblTest1]));
        w8.XOptions = ((Gtk.AttachOptions)(4));
        w8.YOptions = ((Gtk.AttachOptions)(4));
        // Container child table1.Gtk.Table+TableChild
        this.radiobutton1 = new Gtk.RadioButton(Mono.Unix.Catalog.GetString("radiobutton1"));
        this.radiobutton1.CanFocus = true;
        this.radiobutton1.Name = "radiobutton1";
        this.radiobutton1.DrawIndicator = true;
        this.radiobutton1.UseUnderline = true;
        this.radiobutton1.Group = new GLib.SList(System.IntPtr.Zero);
        this.table1.Add(this.radiobutton1);
        Gtk.Table.TableChild w9 = ((Gtk.Table.TableChild)(this.table1[this.radiobutton1]));
        w9.TopAttach = ((uint)(3));
        w9.BottomAttach = ((uint)(4));
        w9.LeftAttach = ((uint)(1));
        w9.RightAttach = ((uint)(2));
        w9.XOptions = ((Gtk.AttachOptions)(4));
        w9.YOptions = ((Gtk.AttachOptions)(4));
        // Container child table1.Gtk.Table+TableChild
        this.radiobutton2 = new Gtk.RadioButton(Mono.Unix.Catalog.GetString("radiobutton2"));
        this.radiobutton2.CanFocus = true;
        this.radiobutton2.Name = "radiobutton2";
        this.radiobutton2.DrawIndicator = true;
        this.radiobutton2.UseUnderline = true;
        this.radiobutton2.Group = this.radiobutton1.Group;
        this.table1.Add(this.radiobutton2);
        Gtk.Table.TableChild w10 = ((Gtk.Table.TableChild)(this.table1[this.radiobutton2]));
        w10.TopAttach = ((uint)(3));
        w10.BottomAttach = ((uint)(4));
        w10.LeftAttach = ((uint)(2));
        w10.RightAttach = ((uint)(3));
        w10.XOptions = ((Gtk.AttachOptions)(4));
        w10.YOptions = ((Gtk.AttachOptions)(4));
        // Container child table1.Gtk.Table+TableChild
        this.radTest1 = new Gtk.RadioButton(Mono.Unix.Catalog.GetString("rad Opt 0"));
        this.radTest1.CanFocus = true;
        this.radTest1.Name = "radTest1";
        this.radTest1.DrawIndicator = true;
        this.radTest1.UseUnderline = true;
        this.radTest1.Group = new GLib.SList(System.IntPtr.Zero);
        this.table1.Add(this.radTest1);
        Gtk.Table.TableChild w11 = ((Gtk.Table.TableChild)(this.table1[this.radTest1]));
        w11.TopAttach = ((uint)(2));
        w11.BottomAttach = ((uint)(3));
        w11.LeftAttach = ((uint)(1));
        w11.RightAttach = ((uint)(2));
        w11.XOptions = ((Gtk.AttachOptions)(4));
        w11.YOptions = ((Gtk.AttachOptions)(4));
        // Container child table1.Gtk.Table+TableChild
        this.radTest2 = new Gtk.RadioButton(Mono.Unix.Catalog.GetString("rad Opt 1"));
        this.radTest2.CanFocus = true;
        this.radTest2.Name = "radTest2";
        this.radTest2.DrawIndicator = true;
        this.radTest2.UseUnderline = true;
        this.radTest2.Group = this.radTest1.Group;
        this.table1.Add(this.radTest2);
        Gtk.Table.TableChild w12 = ((Gtk.Table.TableChild)(this.table1[this.radTest2]));
        w12.TopAttach = ((uint)(2));
        w12.BottomAttach = ((uint)(3));
        w12.LeftAttach = ((uint)(2));
        w12.RightAttach = ((uint)(3));
        w12.XOptions = ((Gtk.AttachOptions)(4));
        w12.YOptions = ((Gtk.AttachOptions)(4));
        // Container child table1.Gtk.Table+TableChild
        this.txtEntry = new Gtk.Entry();
        this.txtEntry.CanFocus = true;
        this.txtEntry.Name = "txtEntry";
        this.txtEntry.Text = Mono.Unix.Catalog.GetString("test text");
        this.txtEntry.IsEditable = true;
        this.txtEntry.InvisibleChar = '●';
        this.table1.Add(this.txtEntry);
        Gtk.Table.TableChild w13 = ((Gtk.Table.TableChild)(this.table1[this.txtEntry]));
        w13.LeftAttach = ((uint)(1));
        w13.RightAttach = ((uint)(2));
        w13.XOptions = ((Gtk.AttachOptions)(4));
        w13.YOptions = ((Gtk.AttachOptions)(4));
        this.hbox1.Add(this.table1);
        Gtk.Box.BoxChild w14 = ((Gtk.Box.BoxChild)(this.hbox1[this.table1]));
        w14.Position = 1;
        w14.Expand = false;
        w14.Fill = false;
        // Container child hbox1.Gtk.Box+BoxChild
        this.vscrollbar1 = new Gtk.VScrollbar(null);
        this.vscrollbar1.Name = "vscrollbar1";
        this.vscrollbar1.Adjustment.Upper = 100;
        this.vscrollbar1.Adjustment.PageIncrement = 10;
        this.vscrollbar1.Adjustment.PageSize = 10;
        this.vscrollbar1.Adjustment.StepIncrement = 1;
        this.hbox1.Add(this.vscrollbar1);
        Gtk.Box.BoxChild w15 = ((Gtk.Box.BoxChild)(this.hbox1[this.vscrollbar1]));
        w15.Position = 2;
        w15.Expand = false;
        w15.Fill = false;
        this.vbox1.Add(this.hbox1);
        Gtk.Box.BoxChild w16 = ((Gtk.Box.BoxChild)(this.vbox1[this.hbox1]));
        w16.Position = 1;
        w16.Expand = false;
        w16.Fill = false;
        // Container child vbox1.Gtk.Box+BoxChild
        this.hscrollbar1 = new Gtk.HScrollbar(null);
        this.hscrollbar1.Name = "hscrollbar1";
        this.hscrollbar1.Adjustment.Upper = 100;
        this.hscrollbar1.Adjustment.PageIncrement = 10;
        this.hscrollbar1.Adjustment.PageSize = 10;
        this.hscrollbar1.Adjustment.StepIncrement = 1;
        this.vbox1.Add(this.hscrollbar1);
        Gtk.Box.BoxChild w17 = ((Gtk.Box.BoxChild)(this.vbox1[this.hscrollbar1]));
        w17.Position = 2;
        w17.Expand = false;
        w17.Fill = false;
        // Container child vbox1.Gtk.Box+BoxChild
        this.progressbar1 = new Gtk.ProgressBar();
        this.progressbar1.Name = "progressbar1";
        this.vbox1.Add(this.progressbar1);
        Gtk.Box.BoxChild w18 = ((Gtk.Box.BoxChild)(this.vbox1[this.progressbar1]));
        w18.Position = 3;
        w18.Expand = false;
        w18.Fill = false;
        // Container child vbox1.Gtk.Box+BoxChild
        this.statusbar1 = new Gtk.Statusbar();
        this.statusbar1.Name = "statusbar1";
        this.statusbar1.Spacing = 6;
        this.vbox1.Add(this.statusbar1);
        Gtk.Box.BoxChild w19 = ((Gtk.Box.BoxChild)(this.vbox1[this.statusbar1]));
        w19.Position = 4;
        w19.Expand = false;
        w19.Fill = false;
        this.Add(this.vbox1);
        if ((this.Child != null)) {
            this.Child.ShowAll();
        }
        this.DefaultWidth = 678;
        this.DefaultHeight = 271;
        this.Show();
        this.DeleteEvent += new Gtk.DeleteEventHandler(this.OnDeleteEvent);
    }
}
