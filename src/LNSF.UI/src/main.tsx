import React from 'react'
import ReactDOM from 'react-dom/client'
import { App } from './App.tsx'
import { BrowserRouter } from 'react-router-dom'
import { AccountProvider, AppThemeProvider, AuthProvider, DrawerProvider, EmergencyContactProvider, PeopleProvider, RoleProvider, RoomProvider, TourProvider } from './Contexts/index.ts'
import { EscortProvider } from './Contexts/escortContext/index.tsx'
import { HospitalProvider } from './Contexts/hospitalContext/index.tsx'
import { TreatmentProvider } from './Contexts/treatmentContext/index.tsx'
import { PatientProvider } from './Contexts/patientContext/index.tsx'
import { HostingProvider } from './Contexts/hostingContext/index.tsx'
import { HostingEscortProvider } from './Contexts/hostingEscortContext/hostingEscortContext.tsx'
import { FamilyGroupProfileProvider } from './Contexts/familyGroupProfileContext/index.tsx'



ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <AppThemeProvider>
      <BrowserRouter>
        <AuthProvider>
          <RoleProvider>
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
                                <FamilyGroupProfileProvider>
                                  <HostingEscortProvider>
                                    <HostingProvider>
                                      <App />
                                    </HostingProvider>
                                  </HostingEscortProvider>
                                </FamilyGroupProfileProvider>
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
          </RoleProvider>
        </AuthProvider>
      </BrowserRouter>
    </AppThemeProvider>
  </React.StrictMode>,
)
