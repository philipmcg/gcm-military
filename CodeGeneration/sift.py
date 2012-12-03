import string
import types
import os, glob
import shutil
from collections import deque
from os.path import basename

def read_file(filename):
	file = open(filename,"r")
	lines = []
	for line in file:
		line = line.rstrip('\n')
		lines.append(line)
	file.close()
	return lines
			   
def write_class(data):
	file = data["file"]
	lines = generate_class(data)
	write = open(file,"w")
	for line in lines:
		print line
		write.write(line)
		write.write("\n")
	write.close()
	
def get_header(cat, data):
	name = data["headername"]
	data["properties"] = prepare_properties(data["fields"])
	
	cat += "\n~" + name + "\n"
	for prop in data["properties"]:
		cat += prop["ids"] + ","
	cat += "\n"
	return cat
	  
def gen_props(list):
	cat = "\n"
	for p in list:
		if p["comment"] != "":
			cat += "/// <summary> \n"
			cat += "/// " + p["comment"] + "\n"
			cat += "/// </summary> \n"
	
		cat += "public %(type)s %(name)s { get; set; }\n" % p
	return cat

def load_methods():
	return  {
		 
		  "s_int":"int.Parse(",
		  "f_int":")",
		  
		  "s_ushort":"ushort.Parse(",
		  "f_ushort":")",
		  
		  "s_string":"",
		  "f_string":"",
		  
		  "s_double":"double.Parse(",
		  "f_double":")",
		  
		  "s_bool":"",
		  "f_bool":" == \"1\" ? true : false ",
		  
		  "s_float":"float.Parse(",
		  "f_float":")",
		}
		
def save_methods():
	return  {
		 
		  "s_int":"",
		  "f_int":".ToString()",
		  
		  "s_ushort":"",
		  "f_ushort":".ToString()",
		  
		  "s_bool":"",
		  "f_bool":" ? \"1\" : \"0\" ",
		  
		  "s_string":"",
		  "f_string":"",
		  
		  "s_double":"",
		  "f_double":".Truncate4()",
		  
		  "s_float":"",
		  "f_float":".Truncate4()",
		}
	
def gen_load(list):
	cat = "string value = null;\n"
	for p in list:
		p["smethod"] = load_methods()["s_" + p["type"]]
		p["fmethod"] = load_methods()["f_" + p["type"]]
		#cat += "\n if(line.ContainsKey(\"%(ids)s\")) \n   this.%(name)s = %(smethod)s line[\"%(ids)s\"] %(fmethod)s;" % p
		
		cat += "\n if(line.TryGetValue(\"%(ids)s\", out value)) \n " % p
		cat += "  this.%(name)s = %(smethod)s value %(fmethod)s;" % p
	return cat
	
def gen_save(list):
	cat = ""
	for p in list:
		p["smethod"] = save_methods()["s_" + p["type"]]
		p["fmethod"] = save_methods()["f_" + p["type"]]
		cat += "\n line[\"%(ids)s\"] = %(smethod)s this.%(name)s %(fmethod)s;" % p
	return cat
	
def gen_usings(list):
	cat = ""
	for p in list:
		cat += "\nusing %(lib)s;" % {"lib" : p[0]}
	return cat
	
def gen_get_field(list):
	cases = ""
	for p in list:
		p["smethod"] = save_methods()["s_" + p["type"]]
		p["fmethod"] = save_methods()["f_" + p["type"]]
		cases += "\n case \"%(ids)s\": \n return %(smethod)s this.%(name)s %(fmethod)s;" % p
	get_field = """
	public string GetField(string name)
	{
		switch(name)
		{
			%(cases)s
			default:
				throw new ArgumentException("No field found with that name", "name");
		}
	}
	""" % {"cases":cases}
	return get_field
	
def pascal_case(s, sep='_'):
	return ''.join([t.title() for t in s.split(sep)])
	
def prepare_properties(list):
	newlist = []
	hascomment = 0
	comment = ""
	
	for	p in list:
		if len(p) == 0:
			continue
		if p[0] == "//":
			hascomment = 1
			p = p[1:len(p)]
			comment = ' '.join(p)
			continue
		
		newlist.append({"comment":(comment if hascomment else ""), "name":pascal_case(p[1]), "ids":(p[1] if len(p) == 2 else p[2]), "type":p[0]})
		if hascomment:
			hascomment = 0
	
	return newlist
	
def generate_class(data):
	lines = []
	def add (line): lines.append(line)
	
	data["usings"] = gen_usings(data["using"]) if "using" in data else ""
	data["properties"] = prepare_properties(data["fields"])
	data["props"] = gen_props(data["properties"])
	data["load"] = gen_load(data["properties"])
	data["save"] = gen_save(data["properties"])
	data["headername"] = data["headername"][0][0]
	if "get_field" in data:
		data["get_field"] = gen_get_field(data["properties"])
	else:
		data["get_field"] = ""
	
	add(
	"""
	
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Military.IO;

%(usings)s

namespace %(namespace)s
{
	
	
	
	
	
	public partial class %(class)s : IMilitaryData
	{
	
	
		%(props)s
		
		public string TagName { get { return XmlTag ; } } 
		public static string XmlTag { get { return "%(headername)s" ; } } 
		
		public %(class)s()
		{
		}
		
		public %(class)s(GCSVLine line)
		{
			Load(line);
		}
		
		public void Load(IGCSVLine line)
		{
			%(load)s
		}
		
		public IGCSVLine SaveAsGCSV(IGCSVHeader header)
		{
			IGCSVLine line = new GCSVLine(header);
			this.Save(line);
			return line;
		}
		
		private void Save(IGCSVLine line)
		{
			%(save)s
		}
			%(get_field)s
			
        public void XmlWriteThis(MilitaryXmlWriter writer) { this.XmlWriteData(writer); }
	}
	
}
	""" % data )
	
	return lines
	
myclass = {
			"filename": "ClassName.cs",
			"class": "ClassName",
			"namespace": "UPBMWinTest",
			"fields":
			  [
			    ["men", "int"],
			    ["training", "int"],
			    ["experience", "double"],
			    ["valor", "double"],
			    ["name", "string"],
			    ["engagements", "int"],
			  ],
		   }

class_definitions = []

for subdir, dirs, files in os.walk(os.getcwd() + "\\classes"):
	for file in files:
		if file.endswith(".txt"):
			base = basename(file).split(".")[0]
			lines = read_file("classes\\" + file)
			dict = {}
			list = []
			for line in lines:
				if line == "":
					continue
				elif " = " in line:
					s = line.split(" = ")
					dict[s[0]] = s[1]
				elif not (" " in line):
					dict[line] = []
					list = dict[line]
				else:
					sublist = filter(None, line.split(" "))
					list.append(sublist)
			if not "file" in dict:
				dict["file"] = base + ".cs"
			if not "class" in dict:
				dict["class"] = base
			class_definitions.append(dict)

for c in class_definitions:
	write_class(c)
		
		
header_text = ""
for c in class_definitions:
	header_text = get_header(header_text, c)
	
write = open("../headers.csv","w")
write.write(header_text)
write.close()

write = open("headers.csv","w")
write.write(header_text)
write.close()
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
			