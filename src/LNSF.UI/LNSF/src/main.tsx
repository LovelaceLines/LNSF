import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import { BrowserRouter } from 'react-router-dom'
import { AccountProvider, AppThemeProvider, AuthProvider, DrawerProvider, EmergencyContactProvider, PeopleProvider, RoomProvider, TourProvider } from './Contexts/index.ts'
import { EscortProvider } from './Contexts/escortContext/index.tsx'
import { HospitalProvider } from './Contexts/hospitalContext/index.tsx'
import { TreatmentProvider } from './Contexts/treatmentContext/index.tsx'
import { PatientProvider } from './Contexts/patientContext/index.tsx'



ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <AppThemeProvider>
      <BrowserRouter>
        <AuthProvider>
          <AccountProvider>
            <DrawerProvider>
              <RoomProvider>
                <PeopleProvider>
                  <EmergencyContactProvider>
                    <TourProvider>
                      <EscortProvider>
                        <HospitalProvider>
                          <TreatmentProvider>
                            <PatientProvider>
                              <App />
                            </PatientProvider>
                          </TreatmentProvider>
                        </HospitalProvider>
                      </EscortProvider>

                    </TourProvider>
                  </EmergencyContactProvider>
                </PeopleProvider>
              </RoomProvider>
            </DrawerProvider>
          </AccountProvider>
        </AuthProvider>
      </BrowserRouter>
    </AppThemeProvider>
  </React.StrictMode>,
)
