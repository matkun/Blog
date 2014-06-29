Language File Editor version 1.1 for EPiServer CMS 6 R2
http://blog.mathiaskunto.com/rss

Installation instructions:

* Drop the files in your solution.
* Update namespaces where necessary.
* Update JavaScript and CSS paths in LanguageFileEditor.aspx if necessary.
* Update Plugin path in LanguageFileEditor.aspx.cs if necessary.
* Update LoadControl path in EditableXmlNode.ascx.cs if necessary.
* Reference Newtonsoft.Json.Net35.dll or later (Available at http://json.codeplex.com/releases/view/64935)
* Make sure that you have secured the plugin so that only administrator accounts can access it.

More information at:
http://blog.mathiaskunto.com/2011/09/04/allowing-web-administrators-to-dynamically-update-episerver-language-files

Well formulated bug reports and constructive feedback is always welcome.

Disclaimer:
Always make backups. You are using this at your own risk,
and cannot hold the author responsible for anything that this may cause.

**************************

Changes in v 1.1

* Security issue.
   Fixed issue where it was possible for web administrators (or everyone, if you keep your plugins in an unsafe place)
   to read, create, modify files on your machine/network shares.
* Code refactoring.

More information at:
http://blog.mathiaskunto.com/2014/06/20/security-fix-for-the-language-file-editor-tool-in-episerver-cms-6-r2/
