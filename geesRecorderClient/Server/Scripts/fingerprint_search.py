#!/usr/bin/env python
# -*- coding: utf-8 -*-

"""
PyFingerprint
Copyright (C) 2015 Bastian Raschke <bastian.raschke@posteo.de>
All rights reserved.
"""

import hashlib
import requests
from pyfingerprint.pyfingerprint import PyFingerprint
from pyfingerprint.pyfingerprint import FINGERPRINT_CHARBUFFER1

STATE_POST_URL = 'https://localhost:5001/fingerprint/state'
ID_POST_URL = 'https://localhost:5001/fingerprint/search/id'

def format_state_url(statusCode):
    return f'{STATE_POST_URL}?statusCode={statusCode}'

def format_id_url(id):
    return f'{ID_POST_URL}?id={id}'

## Search for a finger
##

## Tries to initialize the sensor
try:
    f = PyFingerprint('/dev/ttyUSB0', 57600, 0xFFFFFFFF, 0x00000000)

    if ( f.verifyPassword() == False ):
        raise ValueError('The given fingerprint sensor password is wrong!')

except Exception as e:
    print('The fingerprint sensor could not be initialized!')
    print('Exception message: ' + str(e))
    requests.post(format_state_url(6), verify=False)
    exit(1)

## Gets some sensor information
print('Currently used templates: ' + str(f.getTemplateCount()) +'/'+ str(f.getStorageCapacity()))

## Tries to search the finger and calculate hash
try:
    print('Waiting for finger...')
    requests.post(format_state_url(0),verify=False)
    ## Wait that finger is read
    while ( f.readImage() == False ):
        pass

    ## Converts read image to characteristics and stores it in charbuffer 1
    f.convertImage(FINGERPRINT_CHARBUFFER1)

    ## Searchs template
    result = f.searchTemplate()

    positionNumber = result[0]
    accuracyScore = result[1]

    if ( positionNumber == -1 ):
        print('No match found!')
        requests.post(format_state_url(7),verify=False)
        exit(0)
    else:
        print('Found template at position #' + str(positionNumber))
        print('The accuracy score is: ' + str(accuracyScore))
        requests.post(format_state_url(8),verify=False)
        requests.post(format_id_url(positionNumber), verify=False)

    ## OPTIONAL stuff
    ##

    ## Loads the found template to charbuffer 1
    #f.loadTemplate(positionNumber, FINGERPRINT_CHARBUFFER1)

    ## Downloads the characteristics of template loaded in charbuffer 1
    #characterics = str(f.downloadCharacteristics(FINGERPRINT_CHARBUFFER1)).encode('utf-8')

    ## Hashes characteristics of template
    #print('SHA-2 hash of template: ' + hashlib.sha256(characterics).hexdigest())

except Exception as e:
    print('Operation failed!')
    print('Exception message: ' + str(e))
    requests.post(format_state_url(6), verify=False)
    exit(1)