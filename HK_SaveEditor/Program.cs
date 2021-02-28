using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace HollowKnightdat2json
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			if (args.Length == 0 || args[0].Equals("-h"))
			{
				string str = "";
				str += "本工具用于将空洞骑士的加密.dat文件存档转换成明文状态的.json文件，玩家可以在此处编辑空洞骑士的游戏存档状态。\n";
				str += "HollowKnightdat2json is used to convert the encrypted. dat file of Hollow Knight saves into. json file in plaintext state. Users can edit the game archive state of Hollow Knight here.\n";
				str += "\n\n";
				str += "Usage: \n";
				str += "将.dat文件拖拽至本文件内即可生成.json文件，将.json文件拖拽至本文件内即可生成.dat文件，.dat文件存档在 C:\\Users\\${USER}\\AppData\\LocalLow\\Team Cherry\\Hollow Knight.\n";
				str += "Drag the. dat file here to generate. json file. Drag the. json file into this file to generate. dat file. The. dat file is saved in C:\\Users\\${USER}\\AppData\\LocalLow\\Team Cherry\\Hollow Knight.\n";
				str += "\n\n";
				str += "Made by Megaloasuka.";
				if (args.Length == 0)
				{
					MessageBox.Show(str);
				}
				else
				{
					Console.Write(str);
				}
			}
			else if (args[0].IndexOf(".dat") != -1)
			{
				dat2json(args[0], args[0].Replace(".dat", ".json"));
			}
			else if (args[0].IndexOf(".json") != -1)
			{
				json2dat(args[0], args[0].Replace(".json", ".dat"));
			}
			else
			{
				MessageBox.Show("Error: The file provided is neither a .dat nor a .json file.");
			}
		}

		private static void dat2json(string file, string dest)
		{
			try
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				FileStream fileStream = File.Open(file, FileMode.Open);
				string toDecrypt = (string)binaryFormatter.Deserialize(fileStream);
				fileStream.Close();
				string contents = StringEncrypt.DecryptData(toDecrypt);
				File.WriteAllText(dest, contents);
			}
			catch (Exception ex)
			{
				MessageBox.Show("An error occured: " + ex.Message);
			}
		}

		private static void json2dat(string file, string dest)
		{
			try
			{
				string graph = StringEncrypt.EncryptData(File.ReadAllText(file));
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				FileStream fileStream = File.Open(dest, FileMode.OpenOrCreate);
				binaryFormatter.Serialize(fileStream, graph);
				fileStream.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show("An error occured: " + ex.Message);
			}
		}
	}
}
