//
// Mime.cs: Mime-type method bindings.
//
// Author:
//   Jeroen Zwartepoorte <jeroen@xs4all.nl>
//
// (C) Copyright Jeroen Zwartepoorte 2004
//

using System;
using System.Runtime.InteropServices;

namespace Gnome.Vfs {
	public class MimeType {
		public static readonly string UnknownMimeType = "application/octet-stream";
		private string mimetype;

		[DllImport ("gnomevfs-2")]
		static extern string gnome_vfs_get_mime_type (string uri);
		
		public MimeType (Uri uri)
		{
			mimetype = gnome_vfs_get_mime_type (uri.ToString ());
		}

		[DllImport ("gnomevfs-2")]
		static extern bool gnome_vfs_mime_type_is_known (string mime_type);

		public MimeType (string mimetype)
		{
			if (!gnome_vfs_mime_type_is_known (mimetype))
				throw new ArgumentException ("Unknown mimetype");
			this.mimetype = mimetype;
		}
		
		[DllImport ("gnomevfs-2")]
		static extern string gnome_vfs_get_mime_type_for_data (ref byte data, int size);
		
		public MimeType (byte[] buffer, int size)
		{
			mimetype = gnome_vfs_get_mime_type_for_data (ref buffer[0], size);
		}
		
		[DllImport ("gnomevfs-2")]
		static extern MimeActionType gnome_vfs_mime_get_default_action_type (string mime_type);
		
		[DllImport ("gnomevfs-2")]
		static extern Result gnome_vfs_mime_set_default_action_type (string mime_type, MimeActionType action_type);

		public MimeActionType DefaultActionType {
			get {
				return gnome_vfs_mime_get_default_action_type (mimetype);
			}
			set {
				Result result = gnome_vfs_mime_set_default_action_type (mimetype, value);
				Vfs.ThrowException (result);
			}
		}
		
		[DllImport ("gnomevfs-2")]
		static extern MimeAction gnome_vfs_mime_get_default_action (string mime_type);

		public MimeAction DefaultAction {
			get {
				return gnome_vfs_mime_get_default_action (mimetype);
			}
		}
		
		[DllImport ("gnomevfs-2")]
		static extern string gnome_vfs_mime_get_description (string mime_type);

		[DllImport ("gnomevfs-2")]
		static extern Result gnome_vfs_mime_set_description (string mime_type, string description);

		public string Description {
			get {
				return gnome_vfs_mime_get_description (mimetype);
			}
			set {
				Result result = gnome_vfs_mime_set_description (mimetype, value);
				Vfs.ThrowException (result);
			}
		}
		
		[DllImport ("gnomevfs-2")]
		static extern string gnome_vfs_mime_get_icon (string mime_type);
		
		[DllImport ("gnomevfs-2")]
		static extern Result gnome_vfs_mime_set_icon (string mime_type, string filename);
		
		public string Icon {
			get {
				return gnome_vfs_mime_get_icon (mimetype);
			}
			set {
				Result result = gnome_vfs_mime_set_icon (mimetype, value);
				Vfs.ThrowException (result);
			}
		}

		[DllImport ("gnomevfs-2")]
		static extern bool gnome_vfs_mime_can_be_executable (string mime_type);

		[DllImport ("gnomevfs-2")]
		static extern Result gnome_vfs_mime_set_can_be_executable (string mime_type, bool value);
		
		public bool CanBeExecutable {
			get {
				return gnome_vfs_mime_can_be_executable (mimetype);
			}
			set {
				Result result = gnome_vfs_mime_set_can_be_executable (mimetype, value);
				Vfs.ThrowException (result);
			}
		}
		
		public string Name {
			get {
				return mimetype;
			}
		}

		public override string ToString ()
		{
			return mimetype;
		}
		
		public static string GetMimeTypeForUri (string uri)
		{
			return gnome_vfs_get_mime_type (uri);
		}
	}
}