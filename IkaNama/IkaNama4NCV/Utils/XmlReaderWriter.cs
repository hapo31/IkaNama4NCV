using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace Hapo31.IkaNama.Utils
{
	/// <summary>
	/// Xmlファイルからのシリアライズとオブジェクトのデシリアライズを行う
	/// </summary>
	/// <typeparam name="T">オブジェクトの型</typeparam>
	public class XmlReaderWriter<T>
	{
		private DataContractSerializer ser = new DataContractSerializer(typeof(T));
		private string filename;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="filename">ファイル名</param>
		public XmlReaderWriter(string filename)
		{
			this.filename = filename;
		}

		/// <summary>
		/// Xmlファイルからデシリアライズする
		/// </summary>
		/// <returns></returns>
		public T Read()
		{
			var sr = XmlReader.Create(filename);
			var r = (T)ser.ReadObject(sr);
			sr.Close();
			return r;
		}

		/// <summary>
		/// オブジェクトをシリアライズする
		/// </summary>
		/// <param name="obj">シリアライズするインスタンス</param>
		public void Write(T obj)
		{
			var sw = XmlWriter.Create(filename);
			ser.WriteObject(sw, obj);
			sw.Close();
		}

		/// <summary>
		/// ファイルが存在しているかどうか
		/// </summary>
		/// <returns></returns>
		public bool FileExist()
		{
			return System.IO.File.Exists(filename);
		}
	}
}
