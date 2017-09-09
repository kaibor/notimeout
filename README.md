# NoTimeout v1.0
Patch for Windows 10 systems, which reset the display-timeout counter

## Requirements
[.NET Framework 4.6.2](https://www.microsoft.com/en-US/download/details.aspx?id=53344)

Selected power plan: Balanced (recommended)

# Startup

1. Download the latest executable from [here](https://github.com/kaibor/notimeout/releases).
2. Move the file to a folder that does not bother you.
3. Start it with a double-click.
4. If the program is started successfully, it displays a notification.

![Start notification](/screenshots/notify_start.png)

5. A new notify icon will appear in the taskbar.

![Notify Icon](/screenshots/notifyicon.png)

6. Double-click to open the configuration window directly, or right-click to open the context menu

![Context menu](/screenshots/contextmenu.png)

# Configuration

![Configuration window](/screenshots/configuration.png)

### Timeout (in min) - Set your personal value for display-timeout in minutes.

0 minutes = No timeout.

300 minutes = 5 hours = Maximum

### Refresh (in ms) - Interval in which the value is checked in milliseconds.

60000 milliseconds = 60 seconds = Maximum

### Autostart checkbox

Creates an autostart item or deletes it.

### Notify checkbox

Choose if you want to get notifications about changes or not.

### After the configuration, click on the 'Save' button and the application will restarts itself.
