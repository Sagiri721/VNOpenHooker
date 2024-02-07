import os;
from subprocess import check_output;
import psutil;
import pyperclip;
import time;

library = ".\\library";
junk = " - Shortcut";

print();

index: int = 0;
links = [];
for file in os.listdir(library):

    fullPath = library + "\\" + file;

    if junk in file:
        # Sanitize name
        file = file.replace(junk, "");

        newPath = library + "\\" + file;

        os.rename(fullPath, newPath);
        fullPath = newPath;

    print(f"{index}: {file}");
    links.append(fullPath);

    index += 1;

if len(links) > 1:
    choose = input("What to open: ");

    if not choose.isnumeric(): 
        print("Choose a number");
        exit(1);

    number: int = int(choose);
    if number < 0 or number >= len(links): 
        print("Outside bounds");
        exit(1);
else:
    number = 0;

print("\nOutput: ");

# Execution process
# Run server on another shell
#serverCommand = f'start \"\" ".\\createhook\\x64\\Release\\host.exe" 0';
#print(serverCommand);
#os.system(serverCommand);
# Run game async
runCommand = f"start \"\" \"{links[number]}\"";
print(runCommand);
os.system(runCommand);

# Get the game's process id
gameName = links[number]
gameName = gameName[(gameName.rindex("\\") + 1):(gameName.index(".lnk"))];

pid = None

for proc in psutil.process_iter():
    if gameName in proc.name():
       pid = proc.pid
       break

print(f"pid ({pid}) of {gameName} coppied to clipboard");
pyperclip.copy(pid);

print("\n\n######################################################");
input("Press enter to start injecting the DLL")

# Clear output file
file = open("out.txt", "w");
file.close();


print("\nCompiling injector");
os.system(".\compile.cmd");

print("\nInjecting dll...");
injectionCommand = f".\evil.exe {pid}";
os.system(injectionCommand);