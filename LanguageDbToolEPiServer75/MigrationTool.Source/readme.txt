Automated migration of legacy language files to database initialization classes for the EPiServer language tool
https://blog.mathiaskunto.com/2015/03/19/automated-migration-of-legacy-language-files-to-database-initialization-classes-for-the-episerver-language-tool/

This tool helps you to migrate your legacy EPiServer language xml files to LanguageDbTool initialization classes.

Instructions:

* Place your language.xml files in the root directory (or you will have to supply a path when you run the tool).
* Ensure that you have one language per file.
* Ensure that your language tag has an id attribute containing the IETF language tag for the language.
* Run .\LanguageMigration.exe
   - Generated output files are placed in the .\output directory as default (may be changed by supplying alternate path).
* Go through your language files and:
   - Replace the namespace.
   - Split the translations into proper content groups.
   - Remove the exception.
