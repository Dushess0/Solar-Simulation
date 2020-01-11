
from PIL import Image
import numpy as np
import cv2
from enum import Enum
class State(Enum):
    simulate="0"
    newdata="1"
    action="2"
	reset="100"
	
    def __str__(self):
         return str(self.value)
def check_state():
    f=open("state.txt")
    tmp=f.read() 
    if tmp!='':
        return int(tmp)
    f.close()

   
def change_state(newstate):
    try:
        f=open("state.txt",'w')
        f.write(newstate)
        f.close()
    except:
        pass



change_state("0")

while True:

    if (check_state()==1) :   
        print("read data")
        f= open("ultrasound.txt")
        ultrasound=f.read()
        f.close()
        f= open("lidar.txt")
        lidar=f.read()
        f.close()
        
        f=open("actions.txt",'w')
        f.write("1.0\n-1.0") # lewy silnik 1, prawy -1 => obrot w lewo, 1 1 => do przodu , -1 1 do tylu
        f.close()
        change_state("2")



  
    



        
            


