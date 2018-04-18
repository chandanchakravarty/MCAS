/******************************************************************************************
<Author					: -   Ravindra Gupta
<Start Date				: -	  12/1/2006
<End Date				: -	
<Description			: -   Class library for FTP 
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Sockets;

namespace FtpLib
{

	public class FTPFactory
	{

		private string mRemoteHost,mRemotePath,mRemoteUser,mRemotePassword,mes;
		private int mRemotePort,bytes;
		private Socket clientSocket;
		private int retValue;
		private Boolean mDebugMode;
		private Boolean logined;
		private string reply;
		private static int BLOCK_SIZE = 512;

		Byte[] buffer = new Byte[BLOCK_SIZE];
		Encoding ASCII = Encoding.ASCII;

		public FTPFactory()
		{

			mRemoteHost  = "";
			mRemotePath  = "";
			mRemoteUser  = "";
			mRemotePassword  = "";
			mRemotePort  = 21;
			mDebugMode     = false;
			logined    = false;

		}

		#region Public Properties
		/// <summary>
		/// Set or Get the name of Remote FTP host
		/// </summary>
		public string RemoteHost
		{
			get
			{
				return mRemoteHost;
			}
			set
			{
				mRemoteHost = value;
			}
		}
	
		/// <summary>
		/// 
		/// </summary>
		public int RemotePort
		{
			get
			{
				return mRemotePort;
			}
			set 
			{
				mRemotePort = value;
			}
		}
		
		public string RemotePath 
		{
			get
			{
				return mRemotePath;
			}
			set
			{
				mRemotePath= value;
			}
		}

		public string RemoteUser
		{
			get
			{
				return mRemoteUser;
			}
			set
			{
				mRemoteUser = value;
			}
		}
		public string RemotePassword
		{
			get
			{
				return mRemotePassword;
			}
			set
			{
				mRemotePassword = value;
			}
		}

		public bool InDebugMode 
		{
			get
			{
				return mDebugMode;
			}
			set
			{
				mDebugMode = value;
			}
		}
		
		#endregion

		///
		/// Return a string array containing the remote directory's file list.
		///
		///
		///
		public string[] GetFileList(string mask)
		{

			if(!logined)
			{
				Login();
			}

			Socket cSocket = createDataSocket();

			sendCommand("NLST " + mask);

			if(!(retValue == 150 || retValue == 125))
			{
				throw new IOException(reply.Substring(4));
			}

			mes = "";

			while(true)
			{

				int bytes = cSocket.Receive(buffer, buffer.Length, 0);
				mes += ASCII.GetString(buffer, 0, bytes);

				if(bytes < buffer.Length)
				{
					break;
				}
			}

			char[] seperator = {'\n'};
			string[] mess = mes.Split(seperator);

			cSocket.Close();

			ReadReply();

			if(retValue != 226)
			{
				throw new IOException(reply.Substring(4));
			}
			return mess;

		}

		///
		/// Return the size of a file.
		///
		///
		///
		public long getFileSize(string fileName)
		{

			if(!logined)
			{
				Login();
			}

			sendCommand("SIZE " + fileName);
			long size=0;

			if(retValue == 213)
			{
				size = Int64.Parse(reply.Substring(4));
			}
			else
			{
				throw new IOException(reply.Substring(4));
			}

			return size;

		}

		/// <summary>
		/// Login to the remote server.
		/// </summary>
		public void Login()
		{

			clientSocket = new 
				Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
			IPEndPoint ep = new 
				IPEndPoint(Dns.Resolve(mRemoteHost).AddressList[0], mRemotePort);

			try
			{
				clientSocket.Connect(ep);
			}
			catch(Exception)
			{
				throw new IOException("Couldn't connect to remote server");
			}

			ReadReply();
			if(retValue != 220)
			{
				Close();
				throw new IOException(reply.Substring(4));
			}
			if(mDebugMode)
				Console.WriteLine("USER "+mRemoteUser);

			sendCommand("USER "+mRemoteUser);

			if( !(retValue == 331 || retValue == 230) )
			{
				Cleanup();
				throw new IOException(reply.Substring(4));
			}

			if( retValue != 230 )
			{
				if(mDebugMode)
					Console.WriteLine("PASS xxx");

				sendCommand("PASS "+mRemotePassword);
				if( !(retValue == 230 || retValue == 202) )
				{
					Cleanup();
					throw new IOException(reply.Substring(4));
				}
			}

			logined = true;
			Console.WriteLine("Connected to "+mRemoteHost);

			ChangeDirectory(mRemotePath);

		}

		///
		/// If the value of mode is true, set binary mode for downloads.
		/// Else, set Ascii mode.
		///
		///
		public void SetBinaryMode(Boolean mode)
		{

			if(mode)
			{
				sendCommand("TYPE I");
			}
			else
			{
				sendCommand("TYPE A");
			}
			if (retValue != 200)
			{
				throw new IOException(reply.Substring(4));
			}
		}

		///
		/// Download a file to the Assembly's local directory,
		/// keeping the same file name.
		///
		///
		public void Download(string remFileName)
		{
			Download(remFileName,"",false);
		}

		///
		/// Download a remote file to the Assembly's local directory,
		/// keeping the same file name, and set the resume flag.
		///
		///
		///
		public void Download(string remFileName,Boolean resume)
		{
			Download(remFileName,"",resume);
		}

		///
		/// Download a remote file to a local file name which can include
		/// a path. The local file name will be created or overwritten,
		/// but the path must exist.
		///
		///
		///
		public void Download(string remFileName,string locFileName)
		{
			Download(remFileName,locFileName,false);
		}

		///
		/// Download a remote file to a local file name which can include
		/// a path, and set the resume flag. The local file name will be
		/// created or overwritten, but the path must exist.
		///
		///
		///
		///
		public void Download(string remFileName,string locFileName,Boolean resume)
		{
			if(!logined)
			{
				Login();
			}

			SetBinaryMode(true);

			Console.WriteLine("Downloading file "+remFileName+" from "+mRemoteHost + "/"+mRemotePath);

			if (locFileName.Equals(""))
			{
				locFileName = remFileName;
			}

			if(!File.Exists(locFileName))
			{
				Stream st = File.Create(locFileName);
				st.Close();
			}

			FileStream output = new 
				FileStream(locFileName,FileMode.Open);

			Socket cSocket = createDataSocket();

			long offset = 0;

			if(resume)
			{

				offset = output.Length;

				if(offset > 0 )
				{
					sendCommand("REST "+offset);
					if(retValue != 350)
					{
						//throw new IOException(reply.Substring(4));
						//Some servers may not support resuming.
						offset = 0;
					}
				}

				if(offset > 0)
				{
					if(mDebugMode)
					{
						Console.WriteLine("seeking to " + offset);
					}
					long npos = output.Seek(offset,SeekOrigin.Begin);
					Console.WriteLine("new pos="+npos);
				}
			}

			sendCommand("RETR " + remFileName);

			if(!(retValue == 150 || retValue == 125))
			{
				throw new IOException(reply.Substring(4));
			}

			while(true)
			{

				bytes = cSocket.Receive(buffer, buffer.Length, 0);
				output.Write(buffer,0,bytes);

				if(bytes <= 0)
				{
					break;
				}
			}

			output.Close();
			if (cSocket.Connected)
			{
				cSocket.Close();
			}

			Console.WriteLine("");

			ReadReply();

			if( !(retValue == 226 || retValue == 250) )
			{
				throw new IOException(reply.Substring(4));
			}

		}

		///
		/// Upload a file.
		///
		///
		public void Upload(string fileName)
		{
			Upload(fileName,false);
		}

		///
		/// Upload a file and set the resume flag.
		///
		///
		///
		public void Upload(string fileName,Boolean resume)
		{

			if(!logined)
			{
				Login();
			}

			Socket cSocket = createDataSocket();
			long offset=0;

			if(resume)
			{

				try
				{

					SetBinaryMode(true);
					offset = getFileSize(fileName);

				}
				catch(Exception)
				{
					offset = 0;
				}
			}

			if(offset > 0 )
			{
				sendCommand("REST " + offset);
				if(retValue != 350)
				{
					//throw new IOException(reply.Substring(4));
					//Remote server may not support resuming.
					offset = 0;
				}
			}

			sendCommand("STOR "+Path.GetFileName(fileName));

			if( !(retValue == 125 || retValue == 150) )
			{
				throw new IOException(reply.Substring(4));
			}

			// open input stream to read source file
			FileStream input = new FileStream(fileName,FileMode.Open);

			if(offset != 0)
			{

				if(mDebugMode)
				{
					Console.WriteLine("seeking to " + offset);
				}
				input.Seek(offset,SeekOrigin.Begin);
			}

			Console.WriteLine("Uploading file "+fileName+" to "+mRemotePath);

			while ((bytes = input.Read(buffer,0,buffer.Length)) > 0)
			{

				cSocket.Send(buffer, bytes, 0);

			}
			input.Close();

			Console.WriteLine("");

			if (cSocket.Connected)
			{
				cSocket.Close();
			}

			ReadReply();
			if( !(retValue == 226 || retValue == 250) )
			{
				throw new IOException(reply.Substring(4));
			}
		}

		///
		/// Delete a file from the remote FTP server.
		///
		///
		public void DeleteRemoteFile(string fileName)
		{

			if(!logined)
			{
				Login();
			}

			sendCommand("DELE "+fileName);

			if(retValue != 250)
			{
				throw new IOException(reply.Substring(4));
			}

		}

		///
		/// Rename a file on the remote FTP server.
		///
		///
		///
		public void RenameRemoteFile(string oldFileName,string newFileName)
		{

			if(!logined)
			{
				Login();
			}

			sendCommand("RNFR "+oldFileName);

			if(retValue != 350)
			{
				throw new IOException(reply.Substring(4));
			}

			//  known problem
			//  rnto will not take care of existing file.
			//  i.e. It will overwrite if newFileName exist
			sendCommand("RNTO "+newFileName);
			if(retValue != 250)
			{
				throw new IOException(reply.Substring(4));
			}

		}

		///
		/// Create a directory on the remote FTP server.
		///
		///
		public void MakeDirectory(string dirName)
		{

			if(!logined)
			{
				Login();
			}

			sendCommand("MKD "+dirName);

			if(retValue != 250)
			{
				throw new IOException(reply.Substring(4));
			}

		}

		///
		/// Delete a directory on the remote FTP server.
		///
		///
		public void RemoveDirectory(string dirName)
		{

			if(!logined)
			{
				Login();
			}

			sendCommand("RMD "+dirName);

			if(retValue != 250)
			{
				throw new IOException(reply.Substring(4));
			}

		}

		///
		/// Change the current working directory on the remote FTP server.
		///
		///
		public void ChangeDirectory(string dirName)
		{

			if(dirName.Equals("."))
			{
				return;
			}

			if(!logined)
			{
				Login();
			}

			sendCommand("CWD "+dirName);

			if(retValue != 250)
			{
				throw new IOException(reply.Substring(4));
			}

			this.mRemotePath = dirName;

			Console.WriteLine("Current directory is "+mRemotePath);

		}

		///
		/// Close the FTP connection.
		///
		public void Close()
		{

			if( clientSocket != null )
			{
				sendCommand("QUIT");
			}

			Cleanup();
			Console.WriteLine("Closing...");
		}

		

		private void ReadReply()
		{
			mes = "";
			reply = readLine();
			retValue = Int32.Parse(reply.Substring(0,3));
		}

		private void Cleanup()
		{
			if(clientSocket!=null)
			{
				clientSocket.Close();
				clientSocket = null;
			}
			logined = false;
		}

		private string readLine()
		{

			while(true)
			{
				bytes = clientSocket.Receive(buffer, buffer.Length, 0);
				mes += ASCII.GetString(buffer, 0, bytes);
				if(bytes < buffer.Length)
				{
					break;
				}
			}

			char[] seperator = {'\n'};
			string[] mess = mes.Split(seperator);

			if(mes.Length > 2)
			{
				mes = mess[mess.Length-2];
			}
			else
			{
				mes = mess[0];
			}

			if(!mes.Substring(3,1).Equals(" "))
			{
				return readLine();
			}

			if(mDebugMode)
			{
				for(int k=0;k < mess.Length-1;k++)
				{
					Console.WriteLine(mess[k]);
				}
			}
			return mes;
		}

		private void sendCommand(String command)
		{

			Byte[] cmdBytes = 
				Encoding.ASCII.GetBytes((command+"\r\n").ToCharArray());
			clientSocket.Send(cmdBytes, cmdBytes.Length, 0);
			ReadReply();
		}

		private Socket createDataSocket()
		{

			sendCommand("PASV");

			if(retValue != 227)
			{
				throw new IOException(reply.Substring(4));
			}

			int index1 = reply.IndexOf('(');
			int index2 = reply.IndexOf(')');
			string ipData = reply.Substring(index1+1,index2-index1-1);
			int[] parts = new int[6];

			int len = ipData.Length;
			int partCount = 0;
			string buf="";

			for (int i = 0; i < len && partCount <= 6; i++)
			{

				char ch = Char.Parse(ipData.Substring(i,1));
				if (Char.IsDigit(ch))
					buf+=ch;
				else if (ch != ',')
				{
					throw new IOException("Malformed PASV reply: " + reply);
				}

				if (ch == ',' || i+1 == len)
				{

					try
					{
						parts[partCount++] = Int32.Parse(buf);
						buf="";
					}
					catch (Exception)
					{
						throw new IOException("Malformed PASV reply: " + reply);
					}
				}
			}

			string ipAddress = parts[0] + "."+ parts[1]+ "." +
				parts[2] + "." + parts[3];

			int port = (parts[4] << 8) + parts[5];

			Socket s = new 
				Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
			IPEndPoint ep = new 
				IPEndPoint(Dns.Resolve(ipAddress).AddressList[0], port);

			try
			{
				s.Connect(ep);
			}
			catch(Exception)
			{
				throw new IOException("Can't connect to remote server");
			}

			return s;
		}

	}
}

