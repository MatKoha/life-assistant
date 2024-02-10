import { Button } from '@mui/material';
import React, { createContext, useContext, useState, ReactNode } from 'react';
import './styles.scss';

interface GoogleOAuthContextProps {
    isTokenValid: boolean;
    updateTokenValidity: (isValid: boolean) => void;
}

const GoogleAuthContext = createContext<GoogleOAuthContextProps | undefined>(undefined);

interface GoogleOAuthProviderProps {
    children: ReactNode;
}

export const GoogleOAuthProvider: React.FC<GoogleOAuthProviderProps> = ({ children }) => {
    const [isTokenValid, setTokenValidity] = useState(true);

    const updateTokenValidity = (isValid: boolean) => {
        setTokenValidity(isValid);
    };

    const googleOAuthClient = google.accounts.oauth2.initCodeClient({
        client_id: '630199630528-pd7ocii15jd790bhl7hsnjagd7bvi1pn.apps.googleusercontent.com',
        scope: 'https://www.googleapis.com/auth/tasks https://www.googleapis.com/auth/tasks.readonly https://www.googleapis.com/auth/calendar.readonly',
        ux_mode: 'popup',
        callback: (response) => {
            const xhr = new XMLHttpRequest();
            xhr.open('POST', '/api/google/authorize', true);
            xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
            // Set custom header for CSRF
            xhr.setRequestHeader('X-Requested-With', 'XmlHttpRequest');
            xhr.onload = function () {
                console.log('Google OAuth response: ' + xhr.responseText);
                updateTokenValidity(true);
            };
            xhr.send('code=' + response.code);
        },
    });

    const handleOAuthRequest = () => {
        googleOAuthClient.requestCode();
    };

    return (
        <GoogleAuthContext.Provider value={{ isTokenValid, updateTokenValidity }}>
            {!isTokenValid && (
                <div className="oauth-btn">
                    <Button color="primary" variant="contained" onClick={handleOAuthRequest}>
                        Refresh OAuth Token to view google services
                    </Button>
                </div>
            )}
            {isTokenValid && children}
        </GoogleAuthContext.Provider>
    );
};

export const useGoogleAuthContext = (): GoogleOAuthContextProps => {
    const context = useContext(GoogleAuthContext);

    if (!context) {
        throw new Error('useGoogleAuthContext must be used within a GoogleOAuthProvider');
    }

    return context;
};

export default GoogleOAuthProvider;
