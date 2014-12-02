using System;
using Gtk;
using Interpreter;
using Gdk;
using System.Drawing;

public partial class MainWindow: Gtk.Window
{	
	static Gtk.Image img = new Gtk.Image();
	static Gtk.Label registerLabel = new Gtk.Label ();

	public void SetImageSrc(string src)
	{
		//this.image1.Pixbuf = "";
		img.Pixbuf = new Gdk.Pixbuf (src);
	}

	public void UpdateRegisterOutput(string output)
	{
		registerLabel.Text = output;
	}

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		img = image4;
		registerLabel = label1;
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Gtk.Application.Quit ();
		a.RetVal = true;
	}

	protected void OpenFile (object sender, EventArgs e)
	{
		FileChooserDialog fc = new Gtk.FileChooserDialog (
			"Choose the file to open",
            this,
            FileChooserAction.Open,
            "Cancel", ResponseType.Cancel,
            "Open", ResponseType.Accept
		);

		//fc.Filter = "*.biz";
		if (fc.Run() == (int)ResponseType.Accept) 
		{
			Interpreter.Interpreter.ExecuteProgram (fc.Filename);
		}

		fc.Destroy();
	}

    protected void OnAboutActionActivated(object sender, EventArgs e)
    {
        MessageDialog md = new MessageDialog (
            this,
            DialogFlags.Modal,
            MessageType.Info,
            ButtonsType.Ok,
            "BizAsmGUI\n" +
            "-----------------\n" + 
            "An interpreter for biz files, created by BizAsm assembler.\n" +
            "Developed by Ionică Bizău"
        );
        md.Title = "About";
        md.Run ();
        md.Destroy();
    }
    protected void OnHowToUseActionActivated(object sender, EventArgs e)
    {
        MessageDialog md = new MessageDialog (
            this,
            DialogFlags.Modal,
            MessageType.Info,
            ButtonsType.Ok,
            "Click File > Open, choose the file and open it. "
            + "Then it will be interpreted by this application "
            + "and the result will appear in the console."
        );
        md.Title = "How to use?";
        md.Run ();
        md.Destroy();
    }
}