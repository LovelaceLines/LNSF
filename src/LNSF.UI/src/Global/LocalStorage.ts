import { iUser } from "../Contexts";

export class LocalStorage {
  static getAccessToken(): string | null {
    return localStorage.getItem('@lnsf:accessToken');
  }
  
  static setAccessToken(accessToken: string) {
    localStorage.setItem('@lnsf:accessToken', accessToken);
  }

  static getRefreshToken(): string | null {
    return localStorage.getItem('@lnsf:refreshToken');
  }

  static setRefreshToken(refreshToken: string) {
    localStorage.setItem('@lnsf:refreshToken', refreshToken);
  }

  static clearTokens() {
    localStorage.removeItem('@lnsf:accessToken');
    localStorage.removeItem('@lnsf:refreshToken');
  }

  static getUser(): iUser | null {
    const user = localStorage.getItem('@lnsf:user');

    if (!user) return null;
    return JSON.parse(user);
  }

  static setUser(user: iUser) {
    localStorage.setItem('@lnsf:user', JSON.stringify(user));
  }

  static clearUser() {
    localStorage.removeItem('@lnsf:user');
  }

  static getTryToRefreshToken(): boolean {
    const tryToRefreshToken = localStorage.getItem('@lnsf:tryToRefreshToken');

    if (!tryToRefreshToken) return false;
    return tryToRefreshToken === 'true';
  }
  
  static setTryToRefreshToken() {
    localStorage.setItem('@lnsf:tryToRefreshToken', 'true');
  }

  static clearTryToRefreshToken() {
    localStorage.removeItem('@lnsf:tryToRefreshToken');
  }

  static getMode(): 'light' | 'dark' {
    const mode = localStorage.getItem('@lnsf:mode');

    if (!mode) return 'light';
    return mode === 'dark' ? 'dark' : 'light';
  }

  static setMode(mode: 'light' | 'dark') {
    localStorage.setItem('@lnsf:mode', mode);
  }

  static clearMode() {
    localStorage.removeItem('@lnsf:mode');
  }

  static getPageSize(): number {
    const pageSize = localStorage.getItem('@lnsf:pageSize');

    if (!pageSize) return 100;
    return parseInt(pageSize);
  }

  static setPageSize(pageSize: number) {
    localStorage.setItem('@lnsf:pageSize', pageSize.toString());
  }

  static getColumnVisibilityTour(): { [x: string]: boolean } {
    const columnVisibilityTour = localStorage.getItem('@lnsf:columnVisibilityTour');

    if (!columnVisibilityTour) return { id: false };
    return JSON.parse(columnVisibilityTour);
  }

  static setColumnVisibilityTour(columnVisibilityTour: { [x: string]: boolean }) {
    localStorage.setItem('@lnsf:columnVisibilityTour', JSON.stringify(columnVisibilityTour));
  }

  static getColumnVisibilityPeople(): { [x: string]: boolean } {
    const columnVisibilityPeople = localStorage.getItem('@lnsf:columnVisibilityPeople');

    if (!columnVisibilityPeople) return { id: false };
    return JSON.parse(columnVisibilityPeople);
  }

  static setColumnVisibilityPeople(columnVisibilityPeople: { [x: string]: boolean }) {
    localStorage.setItem('@lnsf:columnVisibilityPeople', JSON.stringify(columnVisibilityPeople));
  }

  static getColumnVisibilityHospital(): { [x: string]: boolean } {
    const columnVisibilityHospital = localStorage.getItem('@lnsf:columnVisibilityHospital');

    if (!columnVisibilityHospital) return { id: false };
    return JSON.parse(columnVisibilityHospital);
  }

  static setColumnVisibilityHospital(columnVisibilityHospital: { [x: string]: boolean }) {
    localStorage.setItem('@lnsf:columnVisibilityHospital', JSON.stringify(columnVisibilityHospital));
  }

  static getColumnVisibilityTreatment(): { [x: string]: boolean } {
    const columnVisibilityTreatment = localStorage.getItem('@lnsf:columnVisibilityTreatment');

    if (!columnVisibilityTreatment) return { id: false };
    return JSON.parse(columnVisibilityTreatment);
  }

  static setColumnVisibilityTreatment(columnVisibilityTreatment: { [x: string]: boolean }) {
    localStorage.setItem('@lnsf:columnVisibilityTreatment', JSON.stringify(columnVisibilityTreatment));
  }

  static getColumnVisibilityHosting(): { [x: string]: boolean } {
    const columnVisibilityHosting = localStorage.getItem('@lnsf:columnVisibilityHosting');

    if (!columnVisibilityHosting) return { id: false };
    return JSON.parse(columnVisibilityHosting);
  }

  static setColumnVisibilityHosting(columnVisibilityHosting: { [x: string]: boolean }) {
    localStorage.setItem('@lnsf:columnVisibilityHosting', JSON.stringify(columnVisibilityHosting));
  }

  static getColumnVisibilityRoom(): { [x: string]: boolean } {
    const columnVisibilityRoom = localStorage.getItem('@lnsf:columnVisibilityRoom');

    if (!columnVisibilityRoom) return { id: false };
    return JSON.parse(columnVisibilityRoom);
  }

  static setColumnVisibilityRoom(columnVisibilityRoom: { [x: string]: boolean }) {
    localStorage.setItem('@lnsf:columnVisibilityRoom', JSON.stringify(columnVisibilityRoom));
  }

  static getColumnVisibilityUser(): { [x: string]: boolean } {
    const columnVisibilityUser = localStorage.getItem('@lnsf:columnVisibilityUser');

    if (!columnVisibilityUser) return { id: false };
    return JSON.parse(columnVisibilityUser);
  }

  static setColumnVisibilityUser(columnVisibilityUser: { [x: string]: boolean }) {
    localStorage.setItem('@lnsf:columnVisibilityUser', JSON.stringify(columnVisibilityUser));
  }

  static getColumnVisibilityFamilyGroupProfile(): { [x: string]: boolean } {
    const columnVisibilityFamilyGroupProfile = localStorage.getItem('@lnsf:columnVisibilityFamilyGroupProfile');

    if (!columnVisibilityFamilyGroupProfile) return { id: false };
    return JSON.parse(columnVisibilityFamilyGroupProfile);
  }

  static setColumnVisibilityFamilyGroupProfile(columnVisibilityFamilyGroupProfile: { [x: string]: boolean }) {
    localStorage.setItem('@lnsf:columnVisibilityFamilyGroupProfile', JSON.stringify(columnVisibilityFamilyGroupProfile));
  }

  static getColumnPeopleRoomHosting(): { [x: string]: boolean } {
    const columnPeopleRoomHosting = localStorage.getItem('@lnsf:columnPeopleRoomHosting');

    if (!columnPeopleRoomHosting) return { id: false };
    return JSON.parse(columnPeopleRoomHosting);
  }

  static setColumnPeopleRoomHosting(columnPeopleRoomHosting: { [x: string]: boolean }) {
    localStorage.setItem('@lnsf:columnPeopleRoomHosting', JSON.stringify(columnPeopleRoomHosting));
  }

  static clearAll() {
    localStorage.clear();
  }
}