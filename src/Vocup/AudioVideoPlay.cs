
using System;
using System.Text;
using System.Runtime.InteropServices;

namespace SimpleAudioVideoPlayback
{
	/// <summary>
	/// Ausnahmebehandlung für das MCI-Interface
	/// </summary>
	public class MCIPlaybackException : ApplicationException
	{
		public MCIPlaybackException() : base()
		{}
		public MCIPlaybackException(string text) : base(text)
		{}
		public MCIPlaybackException(string text, Exception exc) : base(text, exc)
		{}
	}

	/// <summary>
	/// .NET Klasse für die MCI-Library
	/// Wer die Klasse erweitern möchte, kann weiter Kommandos im MSDN finden:
	/// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/multimed/htm/_win32_mci.asp
	/// Die MCI ist recht mächtig. Auch Audio-CDs und Videos können abgespielt werden. 
	/// </summary>
	public class MCIPlayback : IDisposable
	{
		#region DllImport ...
		[DllImport("winmm.dll")]
		private static extern int mciSendString(string cmd, StringBuilder ret, int retLen, IntPtr hwnd);
		[DllImport("winmm.dll")]
		private static extern int mciGetErrorString(int errCode, StringBuilder errText, int errLen); 
		#endregion

		private string name;
		private bool isOpen = false;
		private StringBuilder buffer = new StringBuilder(256);

		public MCIPlayback()
		{
		}

		/// <summary>
		/// Öffnet einen Stream.
		/// </summary>
		/// <param name="file">Datei</param>
		/// <param name="type">Medientyp (siehe Klasssenbeschreibung)</param>
		public void Open(string file, string type)
		{
			name = file;
			string cmd = "open \"" + name + "\" type " + type + " alias \"" + name + "\"";
			int errCode = mciSendString(cmd, null, 0, IntPtr.Zero);
			if(errCode == 0)
				isOpen = true;
			CheckError(errCode);
			
		}

		/// <summary>
		/// Öffnet einen Stream.
		/// </summary>
		/// <param name="type">Medientyp (siehe Klasssenbeschreibung)</param>
		public void Open(string type)
		{
			name = type + "_alias";
			string cmd = "open " + type + " alias " + name;
			int errCode = mciSendString(cmd, null, 0, IntPtr.Zero);
			if(errCode == 0)
				isOpen = true;
			CheckError(errCode);
		}

		/// <summary>
		/// Setzt Zeitformat.
		/// ms = Millisekunden
		/// tmsf = Track Minute Sekunde Frame
		/// </summary>
		/// <param name="timeFormat">Zeitformat</param>
		public void SetTimeFormat(string timeFormat)
		{
			if(!isOpen)
				return;

			string cmd = "set \"" + name + "\" time format " + timeFormat;
			int errCode = mciSendString(cmd, null, 0, IntPtr.Zero);
			CheckError(errCode);
		}

		/// <summary>
		/// Spielt geladenen Stream ab.
		/// </summary>
		public void Play()
		{
			if(!isOpen)
				return;

			string cmd = "play \"" + name + "\"";
			int errCode = mciSendString(cmd, null, 0, IntPtr.Zero);
			CheckError(errCode);
		}

		/// <summary>
		/// Pausiert den Stream.
		/// </summary>
		public void Pause()
		{
			if(!isOpen)
				return;

			string cmd = "pause \"" + name + "\"";
			int errCode = mciSendString(cmd, null, 0, IntPtr.Zero);
			CheckError(errCode);
		}

		/// <summary>
		/// Setzt die Wiedergabe fort.
		/// </summary>
		public void Resume()
		{
			if(!isOpen)
				return;

			string cmd = "resume \"" + name + "\"";
			int errCode = mciSendString(cmd, null, 0, IntPtr.Zero);
			CheckError(errCode);
		}

		/// <summary>
		/// Stopt Wiedergabe
		/// </summary>
		public void Stop()
		{
			if(!isOpen)
				return;

			string cmd = "stop \"" + name + "\"";
			int errCode = mciSendString(cmd, null, 0, IntPtr.Zero);
			CheckError(errCode);
			cmd = "seek \"" + name + "\" to start";
			errCode = mciSendString(cmd, null, 0, IntPtr.Zero);
			CheckError(errCode);
		}

		/// <summary>
		/// Setzt Wiedergabeposition auf den Anfang
		/// </summary>
		public void SeekToStart()
		{
			if(!isOpen)
				return;

			string cmd = "seek \"" + name + "\" to start";
			int errCode = mciSendString(cmd, null, 0, IntPtr.Zero);
			CheckError(errCode);
		}

		/// <summary>
		/// Schließt Stream.
		/// </summary>
		public void Close()
		{
			if(!isOpen)
				return;

			string cmd = "close \"" + name + "\"";
			int errCode = mciSendString(cmd, null, 0, IntPtr.Zero);
			CheckError(errCode);
			isOpen = false;
		}

		/// <summary>
		/// Gibt die Länge des gerade geladenen Mediums zurück.
		/// Kann je nach Format unterschiedlich sein.
		/// </summary>
		/// <returns>Länge in Sekunden</returns>
		public string GetLength()
		{
			if(!isOpen)
				return "";

			string cmd = "status \"" + name + "\" length";
			int errCode = mciSendString(cmd, buffer, 256, IntPtr.Zero);
			CheckError(errCode);
			return buffer.ToString();
		}

		/// <summary>
		/// Gibt die aktuelle Position zurück.
		/// Kann je nach Format unterschiedlich sein.
		/// </summary>
		/// <returns></returns>
		public string GetCurrentPosition()
		{
			if(!isOpen)
				return "";

			string cmd = "status \"" + name + "\" position";
			int errCode = mciSendString(cmd, buffer, 256, IntPtr.Zero);
			CheckError(errCode);
			return buffer.ToString();
		}

		/// <summary>
		/// Öffnet CD-Laufwerk
		/// </summary>
		public void OpenCDDoor()
		{
			string cmd = "set cdaudio door open";
			int errCode = mciSendString(cmd, null, 0, IntPtr.Zero);
			CheckError(errCode);
		}

		/// <summary>
		/// Schließt CD-Laufwerk
		/// </summary>
		public void CloseCDDoor()
		{
			string cmd = "set cdaudio door closed";
			int errCode = mciSendString(cmd, null, 0, IntPtr.Zero);
			CheckError(errCode);
		}

		/// <summary>
		/// Prüft auf Fehler und wirft Exception
		/// </summary>
		/// <param name="code">MCI Fehlercode</param>
		private void CheckError(int code)
		{
			if(code != 0)
			{
				StringBuilder errText = new StringBuilder(256);
				mciGetErrorString(code, errText, 256);
				throw new MCIPlaybackException(errText.ToString());
			}
		}
		
		/// <summary>
		/// Resourcen freigeben
		/// </summary>
		public void Dispose()
		{
			Stop();
			Close();
		}

		/// <summary>
		/// Ist Sream geladen?
		/// </summary>
		public bool IsOpen
		{
			get
			{
				return isOpen;
			}
		}
	}
}