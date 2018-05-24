# -*- coding: utf-8 -*-
"""
Created on Thu May 24 02:39:42 2018

@author: Nikol
"""

import requests
import numpy
import csv
from loss import Loss
from keras.models import model_from_json

g = ['0-', '0+', 'A-', 'A+', 'B-', 'B+', 'AB-', 'AB+']
Omin = [38, 115, 46, 100, 38, 23, 8, 16]
Omax = [78, 240, 96, 210, 82, 50, 18, 36]
Oz = [58, 177, 71, 155, 60, 36, 13, 26]
p = [35, 105, 42, 91, 35, 21, 7, 14]

z = list()
z.append([50, 130, 60, 150, 50, 30, 8, 20])
#z.append([58, 150, 68, 140, 57, 32, 10, 25])
#z.append([44, 90, 50, 66, 40, 23, 6, 18])
#z.append([77, 155, 80, 120, 42, 25, 4, 20])
s = numpy.zeros((4,8))

pz = list() #potrebna zaliha + sredina max i optimalne
pz_cnt = list()

dataset = numpy.loadtxt("donors_new.csv", delimiter=",") 
# split into input (X) and output (Y) variables
X = dataset[:,1:4]

# Model reconstruction from JSON file
with open('model_architecture.json', 'r') as f:
    model = model_from_json(f.read())

# Load weights into the new model
model.load_weights('model_weights.h5')

#predictions = model.predict(X)
## round predictions
#rounded = [round(x[0]*100) for x in predictions]
#print(rounded)

donors = list()
with open('donors.csv', 'r') as csvfile:
    reader = csv.reader(csvfile, delimiter=',')
    for row in reader:
        donors.append(row)

#promjena stanja days_past u bazi ovisno u kojem smo tjednu
days = 0

headers = {'Content-Type': 'application/json'}

called_donors = list()
l = list()

for i in range(0,4):
    predictions = model.predict(X)
    #rounded = [round(x[0]*100) for x in predictions]
    rounded = [x[0] for x in predictions]
    prob = [0, 0, 0, 0, 0, 0, 0, 0]
    
    for j in range(0,8):
        pz.append(Oz[j] - z[i][j] + p[j])
    
    cnt = sum(pz)
    k = 0
    for d in donors:
        if (d[4]=='Z' and (int(d[2])+days)>=120) or (d[4]=='M' and (int(d[2])+days)>=90):
            for j in range(0,8):
                if d[3]==g[j] and prob[j]<=pz[j]:
                    called_donors.append(int(d[0]))
                    prob[j] = prob[j] + rounded[k]
        k = k + 1
        #if(cnt == 0):
        #    break
        
    data = '{"input_ids": ' +str(called_donors)+ ', "days_past": '+ str(days) +'}'
    #print(data)
    pos = requests.post('http://hackaton.westeurope.cloudapp.azure.com/api/evaluate', data=data, headers=headers)
    #print (pos.text)
    ids = pos.text.split(',')
    for did in ids:
        for k in range(0,len(donors)):
            if donors[k][0] == did:
                j = g.index(donors[k][3])
                s[i][j] = s[i][j] + 1
                donors[k][2] = 0
                X[k][1] = 0 
                break
    zi = list()
    for j in range(0,8):
        if z[i][j] + s[i][j] - p[j] > 0:
            zi.append(z[i][j] + s[i][j] - p[j])
        else:
            zi.append(0)
    z.append(zi)
    print(s[i], z[i+1])
    days = days + 7    
    
    with open('donors_solution' + str(i) + '.csv', 'w') as csvoutput:
        writer = csv.writer(csvoutput, delimiter=',')
        writer.writerows([called_donors])
    
    #for k in range(8):    
    #    l.append(Loss.loss(z[i][k], Omin[k], Omax[k]))
        #print (l)
        
    called_donors.clear()
    pz.clear()

#print (sum(l))
    
    