using System;
using Gtk;
using System.Windows.Forms;
using Interpreter;

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
		img = image3;
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Gtk.Application.Quit ();
		a.RetVal = true;
	}

	protected void OpenFile (object sender, EventArgs e)
	{
		OpenFileDialog ofd = new OpenFileDialog ();
		ofd.Filter = "BizMachine|*.biz";
		DialogResult result = ofd.ShowDialog();
		if (result == DialogResult.OK) {
			Interpreter.Interpreter.ExecuteProgram (ofd.FileName);
		}
	}
}