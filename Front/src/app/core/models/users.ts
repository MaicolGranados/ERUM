export interface User {
  id: number;
  username: string;
  email: string;
  isActive: boolean;
  lastLogin: string | null;
  roles: {
    nameRole: string;
  };
}
