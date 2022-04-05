from datetime import date
import sys

arg1 = sys.argv[1].replace('"',"")
arg2 = sys.argv[2].replace('"',"")
arg3 = sys.argv[3].replace('"',"").replace("\\","/").replace(" ","")

try:
    with open(str(arg3)+'/version.dat', 'w') as output:
        output.write(str(arg1[0])+"\n")
        output.write(str(arg1[2])+"\n")
        output.write(date.today().strftime("%m%d")+"\n")
        output.write(str(arg2)+"\n")
except Exception as e:
    print(e,"\n writing bin")

try:
    with open('version.dat', 'w') as output:
        output.write(str(arg1[0])+"\n")
        output.write(str(arg1[2])+"\n")
        output.write(date.today().strftime("%m%d")+"\n")
        output.write(str(arg2)+"\n")
except Exception as e:
    print(e, "\n writing local")