
# coding: utf-8

# In[1]:


import argparse

parser = argparse.ArgumentParser(description='Process some integers.')
parser.add_argument('integers', metavar='N', type=int, nargs='+',
                   help='an integer for the accumulator')


args = parser.parse_args()
print(args.integers)


