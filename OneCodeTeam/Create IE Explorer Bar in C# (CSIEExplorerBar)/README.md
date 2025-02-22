# Create IE Explorer Bar in C# (CSIEExplorerBar)
## Requires
- Visual Studio 2010
## License
- Apache License, Version 2.0
## Technologies
- Internet Explorer
## Topics
- Explorer Bar
## Updated
- 12/05/2011
## Description

<p style="font-family:Courier New">&nbsp;</p>
<h2>Windows APPLICATION: CSIEExplorerBar Overview</h2>
<p style="font-family:Courier New">&nbsp;</p>
<h3>Summary:</h3>
<p style="font-family:Courier New">The sample demonstrates how to create and deploy an IE Explorer Bar which could<br>
list all the images in a web page.<br>
<br>
</p>
<h3>Setup and Removal:</h3>
<p style="font-family:Courier New"><br>
A. Setup<br>
<br>
For 32bit IE on x86 or x64 OS, install CSIEExplorerBarSetup(x86).msi, the output<br>
of the CSIEExplorerBarSetup(x86) setup project.<br>
<br>
For 64bit IE on x64 OS, install CSIEExplorerBarSetup(x64).msi outputted by the <br>
CSIEExplorerBarSetup(x64) setup project.<br>
<br>
B. Removal<br>
<br>
For 32bit IE on x86 or x64 OS, uninstall CSIEExplorerBarSetup(x86).msi, the <br>
output of the CSIEExplorerBarSetup(x86) setup project. <br>
<br>
For 64bit IE on x64 OS, uninstall CSIEExplorerBarSetup(x64).msi, the output of<br>
the CSIEExplorerBarSetup(x64) setup project.<br>
<br>
<br>
</p>
<h3>Demo:</h3>
<p style="font-family:Courier New"><br>
Step1. Open this project in VS2010 and set the platform of the solution to x86. Make<br>
&nbsp; &nbsp; &nbsp; sure that the projects CSIEExplorerBar and CSIEExplorerBarSetup(x86)<br>
&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; are selected to build in Configuration Manager.<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; NOTE: If you want to run this sample in 64bit IE, set the platform to x64 and
<br>
&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; select CSIEExplorerBar and CSIEExplorerBarSetup(x64) to build.<br>
&nbsp; &nbsp; &nbsp; &nbsp;<br>
Step2. Build the solution.<br>
<br>
Step3. Right click the project CSIEExplorerBarSetup(x86) in Solution Explorer, <br>
&nbsp; &nbsp; &nbsp; and choose &quot;Install&quot;.<br>
<br>
&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; After the installation, open 32bit IE and click Tools=&gt;Manage Add-ons, in the
<br>
&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; Manage Add-ons dialog, you can find the item &quot;Image List Explorer Bar&quot;.<br>
<br>
Step4. Open 32bit IE and click Tools=&gt;Explorer Bars, and select &quot;Image List Explorer Bar&quot;.<br>
&nbsp; &nbsp; &nbsp; You will find a Explorer Bar show in the IE.<br>
<br>
Step5. Visit <a href="http://www.microsoft.com/." target="_blank">http://www.microsoft.com/.</a> Click the &quot;Get all images&quot; button in the Explorer<br>
&nbsp; &nbsp; &nbsp; Bar. You will see there are many image urls in the explorer bar. Click one item,<br>
&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; and IE will open a new tab to open the image.<br>
<br>
</p>
<h3>Implementation:</h3>
<p style="font-family:Courier New"><br>
A. Create the project and add references<br>
<br>
In Visual Studio 2010, create a Visual C# / Windows / Class Library project <br>
named &quot;CSIEExplorerBar&quot;. <br>
<br>
Right click the project in Solution Explorer and choose &quot;Add Reference&quot;. Add<br>
&quot;Microsoft.mshtml&quot; in the .NET tab and &quot;Microsoft Internet Controls&quot; in COM tab.<br>
-----------------------------------------------------------------------------<br>
<br>
B. Create IE Explorer Bar.<br>
<br>
To add an Explorer Bar in IE, you can follow these steps:<br>
<br>
1. Create a valid GUID for this ComVisible class. <br>
<br>
2. Implement the IObjectWithSite, IDeskBand, IDockingWindow, IOleWindow and <br>
&nbsp; IInputObject interfaces.<br>
&nbsp; <br>
3. Register this assembly to COM.<br>
<br>
4. 4.Create a new key using the category identifier (CATID) of the type of <br>
&nbsp; Explorer Bar you are creating as the name of the key. This can be one of<br>
&nbsp; the following values: <br>
&nbsp; {00021494-0000-0000-C000-000000000046} Defines a horizontal Explorer Bar. <br>
&nbsp; {00021493-0000-0000-C000-000000000046} Defines a vertical Explorer Bar. <br>
&nbsp; <br>
&nbsp; The result should look like:<br>
<br>
&nbsp; HKEY_CLASSES_ROOT\CLSID\&lt;Your GUID&gt;\Implemented Categories\{00021494-0000-0000-C000-000000000046}<br>
&nbsp; or &nbsp;<br>
&nbsp; HKEY_CLASSES_ROOT\CLSID\&lt;Your GUID&gt;\Implemented Categories\{00021493-0000-0000-C000-000000000046}<br>
&nbsp; <br>
5. Delete following registry keys because they cache the ExplorerBar enum.<br>
<br>
&nbsp; HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Discardable\PostSetup\<br>
&nbsp; Component Categories\{00021493-0000-0000-C000-000000000046}\Enum<br>
&nbsp; or<br>
&nbsp; HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Discardable\PostSetup\<br>
&nbsp; Component Categories\{00021494-0000-0000-C000-000000000046}\Enum<br>
<br>
<br>
6. Set the size of the Explorer Bar in the registry.<br>
<br>
&nbsp; HKEY_LOCAL_MACHINE\Software\Microsoft\Internet Explorer\Explorer Bars\&lt;Your GUID&gt;\BarSize<br>
<br>
<br>
<br>
-----------------------------------------------------------------------------<br>
<br>
D. Deploying the IE Explorer Bar with a setup project.<br>
<br>
&nbsp;(1) In CSIEExplorerBar, add an Installer class (named IEExplorerBarInstaller
<br>
&nbsp; &nbsp; &nbsp;in this code sample) to define the custom actions in the setup. The class derives from<br>
&nbsp; &nbsp; &nbsp;System.Configuration.Install.Installer. We use the custom actions to add/remove the IE
<br>
&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;Context Menu entries in registry and register/unregister the COM-visible classes in<br>
&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;the current managed assembly when user installs/uninstalls the component.
<br>
<br>
&nbsp; &nbsp;[RunInstaller(true), ComVisible(false)]<br>
&nbsp; &nbsp;public partial class IEExplorerBarInstaller : <a class="libraryLink" href="http://msdn.microsoft.com/en-US/library/System.Configuration.Install.Installer.aspx" target="_blank" title="Auto generated link to System.Configuration.Install.Installer">System.Configuration.Install.Installer</a><br>
&nbsp; &nbsp;{<br>
&nbsp; &nbsp; &nbsp; &nbsp;public IEExplorerBarInstaller()<br>
&nbsp; &nbsp; &nbsp; &nbsp;{<br>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;InitializeComponent();<br>
&nbsp; &nbsp; &nbsp; &nbsp;}<br>
&nbsp;<br>
&nbsp; &nbsp; &nbsp; &nbsp;public override void Install(<a class="libraryLink" href="http://msdn.microsoft.com/en-US/library/System.Collections.IDictionary.aspx" target="_blank" title="Auto generated link to System.Collections.IDictionary">System.Collections.IDictionary</a> stateSaver)<br>
&nbsp; &nbsp; &nbsp; &nbsp;{<br>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;base.Install(stateSaver);<br>
<br>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;RegistrationServices regsrv = new RegistrationServices();<br>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;if (!regsrv.RegisterAssembly(this.GetType().Assembly,<br>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;AssemblyRegistrationFlags.SetCodeBase))<br>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;{<br>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;throw new InstallException(&quot;Failed To Register for COM&quot;);<br>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;}<br>
&nbsp; &nbsp; &nbsp; &nbsp;}<br>
<br>
&nbsp; &nbsp; &nbsp; &nbsp;public override void Uninstall(<a class="libraryLink" href="http://msdn.microsoft.com/en-US/library/System.Collections.IDictionary.aspx" target="_blank" title="Auto generated link to System.Collections.IDictionary">System.Collections.IDictionary</a> savedState)<br>
&nbsp; &nbsp; &nbsp; &nbsp;{<br>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;base.Uninstall(savedState);<br>
<br>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;RegistrationServices regsrv = new RegistrationServices();<br>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;if (!regsrv.UnregisterAssembly(this.GetType().Assembly))<br>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;{<br>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;throw new InstallException(&quot;Failed To Unregister for COM&quot;);<br>
&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;}<br>
&nbsp; &nbsp; &nbsp; &nbsp;}<br>
&nbsp; &nbsp;}<br>
<br>
<br>
&nbsp;(2) To add a deployment project, on the File menu, point to Add, and then <br>
&nbsp;click New Project. In the Add New Project dialog box, expand the Other <br>
&nbsp;Project Types node, expand the Setup and Deployment Projects, click Visual <br>
&nbsp;Studio Installer, and then click Setup Project. In the Name box, type <br>
&nbsp;CSIEExplorerBarSetup(x86). Click OK to create the project. <br>
&nbsp;In the Properties dialog of the setup project, make sure that the <br>
&nbsp;TargetPlatform property is set to x86. This setup project is to be deployed
<br>
&nbsp;for 32bit IE on x86 or x64 Windows operating systems. <br>
<br>
&nbsp;Right-click the setup project, and choose Add / Project Output ... <br>
&nbsp;In the Add Project Output Group dialog box, CSIEExplorerBar will &nbsp;<br>
&nbsp;be displayed in the Project list. Select Primary Output and click OK. VS<br>
&nbsp;will detect the dependencies of the CSIEExplorerBar, including .NET<br>
&nbsp;Framework 4.0 (Client Profile).<br>
<br>
&nbsp;Right-click the setup project, and choose View / Custom Actions. <br>
&nbsp;In the Custom Actions Editor, right-click the root Custom Actions node. On <br>
&nbsp;the Action menu, click Add Custom Action. In the Select Item in Project <br>
&nbsp;dialog box, double-click the Application Folder. Select Primary output from
<br>
&nbsp;CSIEExplorerBar. This adds the custom actions that we defined <br>
&nbsp;in IEExplorerBarInstaller of CSIEExplorerBar. <br>
&nbsp;<br>
&nbsp;Build the setup project. If the build succeeds, you will get a .msi file <br>
&nbsp;and a Setup.exe file. You can distribute them to your users to install or <br>
&nbsp;uninstall this Explorer Bar. <br>
<br>
&nbsp;(3) To deploy the Explorer Bar for 64bit IE on a x64 operating system, you <br>
&nbsp;must create a new setup project (e.g. CSIEExplorerBarSetup(x64) <br>
&nbsp;in this code sample), and set its TargetPlatform property to x64. <br>
<br>
&nbsp;Although the TargetPlatform property is set to x64, the native shim <br>
&nbsp;packaged with the .msi file is still a 32-bit executable. The Visual Studio
<br>
&nbsp;embeds the 32-bit version of InstallUtilLib.dll into the Binary table as <br>
&nbsp;InstallUtil. So the custom actions will be run in the 32-bit, which is <br>
&nbsp;unexpected in this code sample. To workaround this issue and ensure that <br>
&nbsp;the custom actions run in the 64-bit mode, you either need to import the <br>
&nbsp;appropriate bitness of InstallUtilLib.dll into the Binary table for the <br>
&nbsp;InstallUtil record or - if you do have or will have 32-bit managed custom <br>
&nbsp;actions add it as a new record in the Binary table and adjust the <br>
&nbsp;CustomAction table to use the 64-bit Binary table record for 64-bit managed
<br>
&nbsp;custom actions. This blog article introduces how to do it manually with <br>
&nbsp;Orca <a href="http://blogs.msdn.com/b/heaths/archive/2006/02/01/64-bit-managed-custom-actions-with-visual-studio.aspx" target="_blank">
http://blogs.msdn.com/b/heaths/archive/2006/02/01/64-bit-managed-custom-actions-with-visual-studio.aspx</a><br>
<br>
&nbsp;In this code sample, we automate the modification of InstallUtil by using a
<br>
&nbsp;post-build javascript: Fix64bitInstallUtilLib.js. You can find the script <br>
&nbsp;file in the CSIEExplorerBarSetup(x64) project folder. To <br>
&nbsp;configure the script to run in the post-build event, you select the <br>
&nbsp;CSIEExplorerBarSetup(x64) project in Solution Explorer, and <br>
&nbsp;find the PostBuildEvent property in the Properties window. Specify its <br>
&nbsp;value to be <br>
&nbsp;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&quot;$(ProjectDir)Fix64bitInstallUtilLib.js&quot; &quot;$(BuiltOuputPath)&quot; &quot;$(ProjectDir)&quot;<br>
<br>
&nbsp;Repeat the rest steps in (2) to add the project output, set the custom <br>
&nbsp;actions, configure the prerequisites, and build the setup project.<br>
<br>
</p>
<h3>Diagnostic:</h3>
<p style="font-family:Courier New"><br>
To debug Explorer Bar, you can attach to iexplorer.exe. <br>
<br>
<br>
<br>
References:<br>
<br>
Creating Custom Explorer Bars, Tool Bands, and Desk Bands<br>
<a href="http://msdn.microsoft.com/en-us/library/bb776819(VS.85).aspx" target="_blank">http://msdn.microsoft.com/en-us/library/bb776819(VS.85).aspx</a><br>
<br>
Adding Explorer Bars<br>
<a href="http://msdn.microsoft.com/en-us/library/aa753590(VS.85).aspx" target="_blank">http://msdn.microsoft.com/en-us/library/aa753590(VS.85).aspx</a></p>
<p style="font-family:Courier New">&nbsp;</p>
<hr>
<div><a href="http://go.microsoft.com/?linkid=9759640" style="margin-top:3px"><img src="http://bit.ly/onecodelogo" alt="">
</a></div>
