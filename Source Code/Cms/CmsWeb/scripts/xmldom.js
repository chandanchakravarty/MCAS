function BaseArray(){this.length=0;}
BaseArray.prototype.addElement = function(v,ref) {
  ref&&(this[ref]=v)
  return this[this.length++]=v;
}
BaseArray.prototype.toString = Array.prototype.toString;
BaseArray.prototype.join = Array.prototype.join;
Array.prototype.add = function (w) {
  this[this.length]=w;
}
/*
BaseArray.prototype.removeElement = function (e,ref) {
  for (var i=0,l=this.length;i!=l;i++) if (this[i]==e) break;
  if (i==l) return null;
  for (;i!=l;i++) this[i]=this[i+1];
  delete this[l];
  if (ref) delete this[ref];
  this.length--;
  return e;
}*/

function DOMNode(name) {this.nodeName=name;}
DOMNode.ELEMENT_NODE=Number(1);
DOMNode.ATTRIBUTE_NODE=Number(2);
DOMNode.TEXT_NODE=Number(3);
DOMNode.CDATA_SECTION_NODE=Number(4);
DOMNode.ENTITY_REFERENCE_NODE=Number(5);
DOMNode.ENTITY_NODE=Number(6);
DOMNode.PROCESSING_INSTRUCTION_NODE=Number(7);
DOMNode.COMMENT_NODE=Number(8);
DOMNode.DOCUMENT_NODE=Number(9);
DOMNode.DOCUMENT_TYPE_NODE=Number(10);
DOMNode.DOCUMENT_FRAGMENT_NODE=Number(11);
DOMNode.NOTATION_NODE=Number(12);
var DOMNp=DOMNode.prototype;
DOMNp.constructor=DOMNode;
DOMNp.attributes=new DOMNamedNodeMap();
DOMNp.childNodes=new DOMNodeList();
DOMNp.nodeType=Number(0);
DOMNp.parentNode=null;
DOMNp.previousSibling=null;
DOMNp.nextSibling=null;
DOMNp.hasChildNodes=function(){return this.childNodes.length!=0;}
DOMNp.hasAttributes=function(){return this.attributes.length!=0;}
DOMNp.appendChild=function(c) {
  if (c.parentNode) return alert('node already appended elsewhere');
  var nodes=this.childNodes,l=nodes.length,n;
  if (!l) this.firstChild=c;
  nodes.addElement(c,c.id);
  c.parentNode=this;
  c.previousSibling=nodes[l-1]||null;
  c.previousSibling && (c.previousSibling.nextSibling=c);
  return this.lastChild=c;
}
DOMNp.removeChild=function(oldC) {
  var c=this.firstChild, i=0, nodes=this.childNodes, l=nodes.length; 
  if (!c) return alert('no children');
  while(c!=oldC && c!=null,i++) c=c.nextSibling;
  if (!c) return alert('childnode not found');
  var prev=nodes[i-1],next=nodes[i+1]; 
  if (!(prev.nextSibling = next||null)) this.lastChild=prev;
  else next.previousSibling = prev;
  for(;i!=l;i++) nodes[i]=nodes[i+1];
  nodes.length--;
  delete nodes[oldC.id];
  oldC.previousSibling=oldC.nextSibling=null;
  return oldC;
}
DOMNp.replaceChild=function(newC,oldC) {
  if (!this.firstChild) return alert('no children');
  for (var c=this.firstChild, i=0;c!=null && c!=oldC; c=c.nextSibling) {
    i++;
  }
  if (c == null) return alert('childnode not found');
  this.childNodes[i]=newC;
  delete this.childNodes[oldC.id];
  this.childNodes[newC.id] = newC;
  var s=newC.previousSibling=oldC.previousSibling;
  s && (s.nextSibling=newC);
  s=newC.nextSibling=oldC.nextSibling;
  s && (s.previousSibling=newC);
  if (i == this.childNodes.length-1) this.lastChild=newC;
  oldC.previousSibling=oldC.nextSibling=null;
  return oldC;
}
DOMNp.cloneNode=function(deep) {
  var d=this.nodeName||this.text||this.data;
  var c=new this.constructor(d,this.ownerDocument);
  var a=this.attributes,l=a.length,i=0;
  for(;i<l;i++) c.attributes.addElement(new DOMAttribute(a[i].name,a[i].value,c));
  if (deep && this.nodeType == 1 && this.childNodes.length) for(i=0;i<this.childNodes.length;i++) {c.appendChild(this.childNodes[i].cloneNode(true))};
  return c;
}
DOMNp.normalize=function(){
  for(var i=0;i<this.childNodes.length;i++) {
    var n=this.childNodes[i];
    if (n.nodeType == DOMNode.TEXT_NODE) {
      if ((s=n.nextSibling) && s.nodeType == DOMNode.TEXT_NODE) {
        n.text+=s.text;
        this.removeChild(s);
        i++;
      }
    }
    else n.normalize();
  }
}
function DOMText(text,ownerDocument) {
  this.ownerDocument=ownerDocument||null;
  this.text=text;
  this.length=text.length;

}
var DTp=DOMText.prototype=new DOMNode('')
DTp.constructor=DOMText;
DTp.nodeType=DOMNode.TEXT_NODE;
DTp.splitText=function(offset) {}
function DOMNodeList() {
  this.length=0;
  this.addElement=BaseArray.prototype.addElement;
}
DOMNodeList.prototype=new BaseArray();
DOMNodeList.prototype.constructor=DOMNodeList;
function DOMNamedNodeMap() {
  this.length=0;
}
DOMNodeList.prototype.item=DOMNamedNodeMap.item=function DOMItem(i) {
  return this[i];
}
var DNNMp=DOMNamedNodeMap.prototype=new BaseArray()
/*
DNNMp.getNamedItem=function(name) {
  return this[name]
}
DNNMp.setNamedItem=function(val) {
  this[
}
*/
function DOMAttribute(name,value,element) {
  this.name=name;
  this.value=value;
  this.ownerElement=element;
}
var DDAp=DOMAttribute.prototype=new DOMNode('');
DDAp.constructor=DOMAttribute;
DDAp.nodeType=DOMNode.ATTRIBUTE_NODE;
DDAp.toString=function(){return this.value};
function DOMElement(name,ownerDocument) {
  this.ownerDocument=ownerDocument||null;
  this.nodeName=name;
  this.attributes=new DOMNamedNodeMap();
  this.childNodes=new DOMNodeList();
}
var DEp=DOMElement.prototype=new DOMNode('');
DEp.constructor=DOMElement;
DEp.nodeType=DOMNode.ELEMENT_NODE;
DEp.getAttribute=function(n){return this.attributes[n]||null}
DEp.setAttribute=function(n,v){
  if (this[n]!=void 0) this[n].value=v;
  else {
    this.attributes.addElement(new DOMAttribute(n,v,this),n);
  }
}
DEp.getElementsByTagName = function (name) {
  var list = new DOMNodeList();
  var checkNode = function (node) {
    if (node.nodeName==name) list.addElement(node,node.id);
    for (var col=node.childNodes,l=col.length,i=0;i!=l;i++) checkNode(col[i])
  }
  checkNode(this)
  return list;
}
function DOMCharacterData(data,ownerDocument) {
  this.ownerDocument=ownerDocument||null;
  this.data=data;
  this.length=data.length;
}
DOMCharacterData.prototype=new DOMNode('CDATA');
DOMCharacterData.prototype.constructor=DOMCharacterData;
DOMCharacterData.prototype.nodeType=DOMNode.CDATA_SECTION_NODE
function DOMComment(data,ownerDocument) {
  this.ownerDocument=ownerDocument||null;
  this.data=data;
  this.length=data.length
}
DOMComment.prototype=new DOMNode('!');
DOMComment.prototype.constructor=DOMComment;
DOMComment.prototype.nodeType=DOMNode.COMMENT_NODE;
function DOMProcessingInstruction(data,target,ownerDocument) {
  this.ownerDocument=ownerDocument||null;
  this.data=data;
  this.target=target;
}
DOMProcessingInstruction.prototype=new DOMNode('?');
DOMProcessingInstruction.prototype.constructor=DOMProcessingInstruction;
DOMProcessingInstruction.prototype.nodeType=DOMNode.PROCESSING_INSTRUCTION_NODE;
function DOMDocument(namespaceURI, qualifiedName, doctype) {
  this.doctype=doctype;
  this.attributes=new DOMNamedNodeMap();
  this.childNodes=new DOMNodeList();
}
var DOMDp=DOMDocument.prototype=new DOMNode('#document');
DOMDp.constructor=DOMDocument;
DOMDp.createElement=function(tagname) {return new DOMElement(tagname,this)}
DOMDp.createProcessingInstruction=function(content) {return new DOMProcessingInstruction(content,this)}
DOMDp.createComment=function(content) {return new DOMComment(content,this)}
DOMDp.createTextNode=function(content) {return new DOMText(content,this)}
DOMDp.createDocumentType=function(qualifiedName, publicId, systemId) {}
DOMDp.createDocument=function(namespaceURI,qualifiedName,doctype) {return new DOMDocument(doctype)}
DOMDp.getElementsByTagName=function(name) {
  return this.documentElement.getElementsByTagName(name);
}
DOMDp.getElementById = function (id) {
  var checkNode = function (node) {
    if (node.id==id) return node;
    for (var targ = null,col=node.childNodes,l=col.length,i=0;i!=l;i++) {
      targ = checkNode(col[i]);
      if (targ) break;
    }
    return targ
  }
  return checkNode(this.documentElement)
}

function XMLHandler () {
}
XMLHandler.prototype.getText=function (element) {
    var a='',e=element.childNodes,l=e.length,i=0;
    for(;i<l;i++) a+=e[i].text||this.getText(e[i])
    return a
}
XMLHandler.prototype.getXML=function(node) {
    var a=[],n=node.childNodes,l=n.length,t=node.attributes,i=0;
    if (node.nodeType==DOMNode.TEXT_NODE) return node.text;
    a.add('<'+node.nodeName);
    for(;i<t.length;i++) a.add(' '+t[i].name+'="'+t[i].value+'"');
    a.add(l ? '>' : '/>');
    for(i=0;i<l;i++) a.add(this.getXML(n[i]));   
    if (l) a.add('</'+node.nodeName+'>');
    return a.join('');
}
XMLHandler.prototype.parseXML=function(source) {
  source = source.replace(/\s+</g,'<').replace(/<[!?][^>]*>/g,'');
  var count = 0;
  var doc=new DOMDocument(),subs, func, cur, cNode, o, t,attReg=/\s*=?\s*\"/;
  var pElm=function(list,doc) {   
    var index = list.indexOf(' ');
    if (index==-1) index = list.length;
    var node = new DOMElement(list.substring(0,index),doc);
    var att=list.substr(index).split(attReg),i=1,l=att.length;
    for(;i<l;i+=2) {
      var name = att[i-1].substr(1)
      node.setAttribute(name,att[i]);
      if (name.toLowerCase()=='id') {
        node.id = att[i];
        }
      }
    return node;
    
  }
  split=source.split('>');     
  for(var l = split.length,i=0;i!=l;i++) {
    subs=split[i].split('<');
    o=subs[0];
    t=subs[1];
    if (o.length) cNode.appendChild(doc.createTextNode(o));
    if (t) {
      if (t.charAt(t.length-1) == '/') {
        if (!cNode) cNode=doc.documentElement=pElm(t.substring(0,t.length-1),doc)
        else cNode.appendChild(pElm(t.substring(0,t.length-1),doc))
      }
      else {
        if (t.charAt(0)!='/') {
          if (!i) {
            cNode=doc.documentElement=doc.appendChild(pElm(t,doc));
          }
          else {
            cNode=cNode.appendChild(pElm(t,doc));
          }
        }
        else {
          if (t.substring(1,t.length)!=(cNode.nodeName)) return alert('Error: end tag: <'+t+'> does not match start tag: <'+cNode.nodeName+'>');
          else cNode=cNode.parentNode;
        }
      }
    }
  }
return doc
}

//this constructor is used for quick parsing (does not use attributes but pointers instead)
function DOMLightElement(name,ownerDocument) {
  this.ownerDocument=ownerDocument||null;
  this.nodeName=name;
  this.childNodes=new DOMNodeList();
  this.attributes = this;
}
DOMLightElement.prototype=new DOMElement('');
String.prototype.isEmpty = function () {
  if (!this.length) return true
  if (this.match(/^\s+$/)) return true
  return false
}
XMLHandler.prototype.quickParseXML=function(source) {
  source = source.replace(/\s+</g,'<').replace(/<[!?][^>]*>/g,'');
  var count = 0;
  var doc=new DOMDocument(),subs, func, cur, cNode, o, t,attReg=/\s*=?\s*\"[^\"]*\"\s*/, valReg=/\"[^\"]*\"/g;
  if (source.isEmpty()) return alert('Parse error: empty document received');
  var pElm=function(list,doc) {  
    var index = list.indexOf(' ');
    if (index==-1) index = list.length;
    var node = new DOMLightElement(list.substring(0,index),doc);
    var att=list.substr(index+1).split(attReg),i=0,l=att.length;
    var vl=list.match(valReg);
    for(;vl&&i<l;i++) {
      if (!att[i]) continue;
      node[att[i]]=vl[i].substr(1, vl[i].length-2);
    } 
      return node;
  }
  split=source.split('>');     
  for(var l = split.length,i=0;i!=l;i++) {
    subs=split[i].split('<');
    o=subs[0];
    t=subs[1];
    if (o.length) cNode.appendChild(doc.createTextNode(o));
    if (t) {
      if (t.charAt(t.length-1) == '/') {
        if (!cNode) cNode=doc.documentElement=pElm(t.substring(0,t.length-1),doc)
        else cNode.appendChild(pElm(t.substring(0,t.length-1),doc))
        
      }
      else {
        if (t.charAt(0)!='/') {
          if (!i) {
            cNode=doc.documentElement=doc.appendChild(pElm(t,doc));
          }
          else {
            cNode=cNode.appendChild(pElm(t,doc));
          }
        }
        else {
          if (t.substring(1,t.length)!=(cNode.nodeName)) return alert('Error: end tag: <'+t+'> does not match start tag: <'+cNode.nodeName+'>');
          else cNode=cNode.parentNode;
        }
      }
    }
  }
return doc
}

