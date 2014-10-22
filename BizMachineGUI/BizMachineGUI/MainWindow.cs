using System;
using Gtk;
using Interpreter;

public partial class MainWindow: Gtk.Window
{	
	static Gtk.Image img = new Gtk.Image();
	static Gtk.Label registerLabel = new Gtk.Label ();

	public void SetImageSrc(string src) {
		//this.image1.Pixbuf = "";
		img.Pixbuf = new Gdk.Pixbuf (src);
	}

	public void UpdateRegisterOutput(string output) {
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
		FileChooserDialog fc = new Gtk.FileChooserDialog ("Choose the file to open",
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
}