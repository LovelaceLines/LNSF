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

  static clearAll() {
    localStorage.clear();
  }
}