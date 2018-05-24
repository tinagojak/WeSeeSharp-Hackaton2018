# -*- coding: utf-8 -*-
"""
Created on Thu May 24 00:16:08 2018

@author: Nikol
"""

# Create your first MLP in Keras
from keras.models import Sequential
from keras.layers import Dense
import numpy
# fix random seed for reproducibility
numpy.random.seed(7)
# load pima indians dataset
dataset = numpy.loadtxt("donors_valid.csv", delimiter=",")
# split into input (X) and output (Y) variables
X = dataset[:,1:4]
Y = dataset[:,4]
# create model
model = Sequential()
model.add(Dense(5, input_dim=3, activation='relu'))
model.add(Dense(8, activation='relu'))
model.add(Dense(1, activation='sigmoid'))
# Compile model
model.compile(loss='binary_crossentropy', optimizer='adam', metrics=['accuracy'])
#model.compile(loss='mean_squared_error', optimizer='sgd', metrics=['accuracy'])
# Fit the model
model.fit(X, Y, epochs=3, batch_size=10)
# evaluate the model
scores = model.evaluate(X, Y)
#print("\n%s: %.2f%%" % (model.metrics_names[1], scores[1]*100))

predictions = model.predict(X)
# round predictions
rounded = [round(x[0]*100) for x in predictions]
#print(rounded)

# Save the weights
model.save_weights('model_weights.h5')

# Save the model architecture
with open('model_architecture.json', 'w') as f:
    f.write(model.to_json())

#i = 0 
#numpy.savetxt('donor_trained.csv', dataset, delimiter=",")
#with open('donors_valid.csv', 'r') as csvfile:
#    with open('donors_trained.csv', 'w') as csvoutput:
#        writer = csv.writer(csvoutput, delimiter=',', lineterminator='\n')
#        reader = csv.reader(csvfile, delimiter=',')
#        all = []
#        for row in reader:
#            row.append(rounded[i])
#            i = i + 1 
#            all.append(row)
#        writer.writerows(all)