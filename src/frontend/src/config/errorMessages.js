// src/config/errorMessages.js

const errorMessages = {
    '00': 'Transaction successful.',
    '07': 'Transaction successful. However, the transaction is flagged as suspicious (related to fraud or unusual activity).',
    '09': 'Transaction failed: Customer’s card/account is not registered for Internet Banking at the bank.',
    '10': 'Transaction failed: Customer has entered incorrect card/account information more than 3 times.',
    '11': 'Transaction failed: Payment timeout. Please try the transaction again.',
    '12': 'Transaction failed: Customer’s card/account is locked.',
    '13': 'Transaction failed: Incorrect transaction authentication password (OTP) entered. Please try the transaction again.',
    '24': 'Transaction failed: Customer canceled the transaction.',
    '51': 'Transaction failed: Insufficient balance in the customer’s account.',
    '65': 'Transaction failed: Customer’s account has exceeded the daily transaction limit.',
    '75': 'Payment gateway is under maintenance.',
    '79': 'Transaction failed: Customer has entered payment password incorrectly more than the allowed number of times. Please try the transaction again.',
    '99': 'Other errors (remaining errors not listed in the provided error code list).',
};

export default errorMessages;
