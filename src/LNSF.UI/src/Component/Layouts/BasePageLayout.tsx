import { AppBar, Avatar, Box, Button, Container, Divider, Paper, Popover, Toolbar, Typography, useMediaQuery, useTheme } from "@mui/material"
import MenuIcon from '@mui/icons-material/Menu';
import Brightness6RoundedIcon from '@mui/icons-material/Brightness6Rounded';
import iconelogoProvisoria from '../../assets/icone_logo.svg';
import { AuthContext, iUser, useAppThemeContext, useDrawerContext } from "../../Contexts";
import LogoutIcon from '@mui/icons-material/Logout';
import { useContext, useEffect, useState } from "react";

interface IBasePageLayoutProps { children: React.ReactNode; }

export const BasePageLayout: React.FC<IBasePageLayoutProps> = ({ children }) => {
  const theme = useTheme();
  const smDown = useMediaQuery(theme.breakpoints.down('sm'));
  const mdDown = useMediaQuery(theme.breakpoints.down('md'));
  const { isDrawerOpen, toggleDrawerOpen, } = useDrawerContext();
  const { toggleTheme } = useAppThemeContext();
  const { getUser,  logout } = useContext(AuthContext)
  const [user, setUser] = useState<iUser | null>(null);

  useEffect(() => {
    const fetchUser = async () => setUser(await getUser());
    
    fetchUser();
  }, []);

  const [popoverAnchorEl, setPopoverAnchorEl] = useState<null | HTMLElement>(null);

  const handlePopoverOpen = (event: React.MouseEvent<HTMLElement>) => {
    setPopoverAnchorEl(event.currentTarget);
  };

  const handlePopoverClose = () => {
    setPopoverAnchorEl(null);
  };

  const handleLogout = () => {
    logout();
  };

  const displayRoles = (): string => {
    if (!user) return '';

    let roles = user.roles;
    if (roles.length > 2) {
      roles = roles.slice(0, 2);
      roles.push('etc');
    }

    return roles.join(', ') || '';
  };

  const openPopover = Boolean(popoverAnchorEl);

  const topBar = (
    <AppBar 
      component={Toolbar}
      position='relative'
      color='inherit'
      enableColorOnDark={true}
      sx={{ flexDirection: 'row', alignItems: 'center', justifyContent: 'flex-end', gap: 2 }}
    >
      {smDown && (
        <Button
          color='primary'
          onClick={toggleDrawerOpen}
          sx={{ marginLeft: 0, marginRight: 'auto', paddingLeft: 0, minWidth: 'min-content' }}
        >
          <MenuIcon sx={{ marginLeft: 0, paddingLeft: 0 }}/>
        </Button>
      )}
      
      <Button onClick={toggleTheme}>
        <Brightness6RoundedIcon color='primary' />
      </Button>
    
      {!smDown && !!user && (
        <Box display='flex' flexDirection='column' gap={1}>
          <Typography variant={mdDown ? 'body2' : 'subtitle1'}>
            {user.userName}
          </Typography>

          <Typography variant={mdDown ? 'body2' : 'subtitle1'}>
            {displayRoles()}
          </Typography>
        </Box>
      )}

      <Box onClick={handlePopoverOpen}>
        <Avatar src={iconelogoProvisoria} sx={{ width: 30, height: 30 }} />
      </Box>
      <Popover
        open={openPopover}
        anchorEl={popoverAnchorEl}
        onClose={handlePopoverClose}
        anchorOrigin={{
            vertical: 'bottom',
            horizontal: 'left',
        }}
      >
        <Box padding={2}
          display= 'flex'
          flexDirection='column'
          alignItems='center'
          justifyContent= 'center'
        >
          <Box
            marginBottom={2}>
            <Typography
              variant={mdDown ? 'body2' : 'subtitle1'}
              textAlign='center'
            >
              {/* {user.role.toString()} */}
              <Typography fontSize={12}>
                  {!!user && user.email}
              </Typography>
            </Typography>
          </Box>

          <Divider />

          <Button 
            onClick={handleLogout}
            startIcon={<LogoutIcon color='primary' />}
          >
            Sair
          </Button>
        </Box>
      </Popover>
    </AppBar>
  );

  const childrenContent = (
    <Box
      id="ChildrenContentContainer"
      component={Container}
      disableGutters={smDown}
      maxWidth={smDown ? 'false' : 'xl'}
    >
      <Box
        id="ChildrenContentPaper"
        component={Paper}
      >
        {children}
      </Box >
    </Box >
  );

  return (
    <Box
      id="BasePageLayout" 
      display='flex'
      flexDirection='column'
      gap={smDown ? 1 : 4}
    >
      {topBar}
      {childrenContent}
    </Box>
  )
}