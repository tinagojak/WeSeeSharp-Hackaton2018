#!/usr/bin/env python2
# -*- coding: utf-8 -*-
"""
Created on Wed May 23 21:11:38 2018

@author: tina
"""

import requests
import random
import numpy as np
import csv

headers = {'Content-Type': 'application/json'}
i=0
donors = list()
with open('donors.txt', 'rb') as csvfile:
    reader = csv.reader(csvfile, delimiter=',')
    for row in reader:
        donors.append(row)
donors_valid = list()
donors_valid_ids = list()

while i<100:
    days = np.random.randint(0,300)
    for d in donors:
        if (d[4]=='Z' and (int(d[2])+days)>=120) or (d[4]=='M' and (int(d[2])+days)>=90):
            donors_valid.append((int(d[0]), d[1], (int(d[2])+days), d[5]))
            donors_valid_ids.append(int(d[0]))
    count = np.random.randint(0,len(donors_valid_ids))
    ids = str(random.sample(donors_valid_ids, count))
    data = '{"input_ids": ' +ids+ ', "days_past": '+ str(days) +'}'
    p = requests.post('http://hackaton.westeurope.cloudapp.azure.com/api/evaluate', data=data, headers=headers)
    y = p.text
    y = y.split(',')
    with open('donors_valid.csv', 'wb') as csvfile:
        fieldnames = ['id', 'frequency', 'days_past', 'distance', 'y']
        writer = csv.writer(csvfile, delimiter=',', quoting=csv.QUOTE_MINIMAL)
        writer.writerow(('id', 'frequency', 'days_past', 'distance', 'y'))
        for d in donors_valid:
            if str(d[0]) in y:
                pp = 1
            else:
                pp = 0
            writer.writerow((d[0], d[1], d[2],d[3], pp))
    i = i+1

print 'DONE'