using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{	
	static Gtk.Image img = new Gtk.Image();
	public void SetImageSrc(string src) {
		//this.image1.Pixbuf = "";
		img.Pixbuf = new Gdk.Pixbuf (src);
	}

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		img = image2;

	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
