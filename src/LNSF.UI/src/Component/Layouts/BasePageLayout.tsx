import { Avatar, Box, Button, Container, Divider, Paper, Popover, Typography, useMediaQuery, useTheme } from "@mui/material"
import MenuIcon from '@mui/icons-material/Menu';
import MenuOpenIcon from '@mui/icons-material/MenuOpen';
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
    const fetchUser = async () => {
      try {
        setUser(await getUser());
      } catch (error) {
        console.log("error useEffect BasePageLayout: ", error);
      }
    };
    
    fetchUser();
  }, [getUser]);

  const [popoverAnchorEl, setPopoverAnchorEl] = useState<null | HTMLElement>(null);

  const handlePopoverOpen = (event: React.MouseEvent<HTMLElement>) => {
    setPopoverAnchorEl(event.currentTarget);
  };

  const handlePopoverClose = () => {
    setPopoverAnchorEl(null);
  };

  const handleLogout = () => {
    logout();
  }

  const openPopover = Boolean(popoverAnchorEl);

  const topBar = (
    <Box 
      component={Paper}
    >
      <Box
        component={Container}
        height={theme.spacing(mdDown ? 9 : 10)}
        display='flex'
        justifyContent={smDown ? 'space-between' : 'end'}
        alignItems='center'
      >
        {smDown && (
          <Button
            color='primary'
            onClick={toggleDrawerOpen}
            sx={{ marginLeft: 0, paddingLeft: 0, minWidth: 'min-content' }}
          >
            <MenuIcon
              sx={{ marginLeft: 0, paddingLeft: 0 }}
            />
          </Button>
        )}

        <Box
          display='flex'
          alignItems='center'
          gap={2}
        >
          <Button onClick={toggleTheme}  >
            <Brightness6RoundedIcon color='primary' />
          </Button>
        
          {user && (
            <Box>
              <Typography variant={mdDown ? 'body2' : 'subtitle1'}>
                {user.userName}
              </Typography>

              <Typography variant={mdDown ? 'body2' : 'subtitle1'}>
                {user.roles.join(', ')}
              </Typography>
            </Box>
          )}

          <Box onClick={handlePopoverOpen}>
              <Avatar alt="Remy Sharp" src={iconelogoProvisoria} sx={{ width: 30, height: 30 }} />
          
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
                          {user && user.email}
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
        </Box>
      </Box>
    </Box>
  );

  const childrenContent = (
    <Box
      id="ChildrenContentContainer"
      component={Container}
    >
      <Box
        id="ChildrenContentPaper"
        component={Paper}
        marginX={smDown ? 2 : mdDown ? 5 : 8}
        gap={1}
      >
        {children}
      </Box >
    </Box >
  );

  return (
    <Box
      id="BasePageLayout" 
      width="100%"
      height="100%"
      display='flex'
      flexDirection='column'
      gap={4}
    >
      {topBar}
      {childrenContent}
    </Box>
  )
}