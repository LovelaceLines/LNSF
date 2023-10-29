import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import { BrowserRouter } from 'react-router-dom'
import {  AccountProvider, AppThemeProvider, AuthProvider, DrawerProvider, PeopleProvider, RoomProvider, TourProvider } from './Contexts/index.ts'



ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <AppThemeProvider>
      <BrowserRouter>
        <AuthProvider>
          <AccountProvider>
          <DrawerProvider>
            <RoomProvider>
              <PeopleProvider>
                <TourProvider>
              
                    <App />
                 
                </TourProvider>
              </PeopleProvider>
            </RoomProvider>
          </DrawerProvider>
          </AccountProvider>
        </AuthProvider>
      </BrowserRouter>
    </AppThemeProvider>
  </React.StrictMode>,
)
