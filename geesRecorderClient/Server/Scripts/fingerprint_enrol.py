import time
import requests
from pyfingerprint.pyfingerprint import PyFingerprint
from pyfingerprint.pyfingerprint import FINGERPRINT_CHARBUFFER1
from pyfingerprint.pyfingerprint import FINGERPRINT_CHARBUFFER2

STATE_POST_URL = 'https://localhost:5001/fingerprint/state'
ID_POST_URL = 'https://localhost:5001/fingerprint/enrol/id'

def format_state_url(statusCode):
    return f'{STATE_POST_URL}?statusCode={statusCode}'

def format_id_url(id,exists):
    return f'{ID_POST_URL}?id={id}&exists={exists}'


## Enrolls new finger
##

## Tries to initialize the sensor - '/dev/ttyUSB0' - (Linux port)
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

## Tries to enroll new finger
try:
    print('Waiting for finger...')    
    requests.post(format_state_url(0),verify=False)
    
    ## Wait that finger is read
    while ( f.readImage() == False ):
        pass

    ## Converts read image to characteristics and stores it in charbuffer 1
    f.convertImage(FINGERPRINT_CHARBUFFER1)

    ## Checks if finger is already enrolled
    result = f.searchTemplate()
    positionNumber = result[0]

    if ( positionNumber >= 0 ):

        requests.post(format_state_url(3), verify=False)
        requests.post(format_id_url(positionNumber, True), verify=False)

        print('Template already exists at position #' + str(positionNumber))
        exit(0)

    print('Remove finger...')

    requests.post(format_state_url(2), verify=False)
    
    time.sleep(2)

    requests.post(format_state_url(1), verify=False)
    print('Waiting for same finger again...')

    ## Wait that finger is read again
    while ( f.readImage() == False ):
        pass

    ## Converts read image to characteristics and stores it in charbuffer 2
    f.convertImage(FINGERPRINT_CHARBUFFER2)

    ## Compares the charbuffers
    if ( f.compareCharacteristics() == 0 ):
        raise Exception('Fingers do not match')

    ## Creates a template
    f.createTemplate()

    ## Saves template at new position number
    positionNumber = f.storeTemplate()
    print('Finger enrolled successfully!')
    print('New template position #' + str(positionNumber))

    requests.post(format_state_url(5), verify=False)
    requests.post(format_id_url(positionNumber,False), verify=False)

except Exception as e:
    print('Operation failed!')
    print('Exception message: ' + str(e))

    requests.post(format_state_url(6), verify=False)
    exit(1)