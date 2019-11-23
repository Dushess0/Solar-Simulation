from mlagents.envs.environment import UnityEnvironment

train_mode = False

env = UnityEnvironment(file_name="compiled-LINUX/BoatSim")
# [left,right] left - left and right engines signal (1 means forward, 0 do nothing,-1 backward)
env.reset(train_mode=train_mode, config=None)
env.step([1,1], memory=None, text_action=None) # move forward for one step



env.close() # close enviroment