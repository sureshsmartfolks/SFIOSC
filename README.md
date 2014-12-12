SFIOSC
======

OSC_STORM Project
1. Launch Storm.sln in STORMSFI-master
2. Right Click on OSC.Services.ProjectManagement Properties, Goto Web Settings, specify IIS Express project url port number and create virtual directory.(Ex: http://localhost:50833)
3. Right Click on ProjectManagementWeb Properties, Goto Web Settings, specify IIS Express project url port number and and create virtual directory.(Ex: http://localhost:50824)
4. Change OSC.Services.ProjectManagementWeb Web.Config Settings. Server, Database & Username-Password Details.
5. Dont Configure/Create API's in IIS Manager. By Default Service&Web API will install automatically once you run the project.
6. Save All Settings and Build Solution.
7. Set OSC.Services.ProjectManagementWeb as Startup Project.
8. Run with Chrome Browser/IE 11 Browser.
9. After Service started in browser, then type your service port number followed by html page.
Ex: http://localhost:50824/demo.html
      http://(localhost:Web IIS Server PortNumber/Htmlpage)
10. Newly Added Files:
	JpList JQuery Plugin Pre-Installed.
	JavaScript: STORMSFI-master/Web/ProjectManagement/Scripts/jplist & vendor Folder.
	StyleSheet: STORMSFI-master/Web/ProjectManagement/Styles/css & vendor Folder.
	HTML: STORMSFI-master/Web/ProjectManagement/demo.html & list.html.
11. For database config run the dev_storm script file.
