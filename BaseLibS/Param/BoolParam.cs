﻿using System;
using System.Globalization;
using System.Xml;

namespace BaseLibS.Param{
	[Serializable]
	public class BoolParam : Parameter<bool>{
        /// <summary>
        /// for xml serialization only
        /// </summary>
	    public BoolParam() : this("") { }

	    public BoolParam(string name) : this(name, false){}

		public BoolParam(string name, bool value) : base(name){
			Value = value;
			Default = value;
		}

		public override string StringValue{
			get { return Value.ToString(CultureInfo.InvariantCulture); }
			set { Value = bool.Parse(value); }
		}

		public override void Clear(){
			Value = false;
		}
		public override ParamType Type => ParamType.Server;
	}
}