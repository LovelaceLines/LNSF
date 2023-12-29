import { Avatar, Box, Button, Container, Divider, Paper, Popover, Typography, useMediaQuery, useTheme } from "@mui/material"
import MenuIcon from '@mui/icons-material/Menu';
import MenuOpenIcon from '@mui/icons-material/MenuOpen';
import Brightness6RoundedIcon from '@mui/icons-material/Brightness6Rounded';
import iconelogoProvisoria from '../../assets/icone_logo.svg';
import { AuthContext, iUser, useDrawerContext } from "../../Contexts";
import LogoutIcon from '@mui/icons-material/Logout';
import { useContext, useEffect, useState } from "react";
import { Footer } from "./footer/Footer";

interface IBasePageLayoutProps {
  children: React.ReactNode;
}

export const BasePageLayout: React.FC<IBasePageLayoutProps> = ({ children }) => {
    const theme = useTheme();
    const smDown = useMediaQuery(theme.breakpoints.down('sm'));
    const mdDown = useMediaQuery(theme.breakpoints.down('md'));
    const { isDrawerOpen, toggleDrawerOpen, } = useDrawerContext();
    // const { toggleTheme } = useAppThemeContext();
    const { getUser,  logout } = useContext(AuthContext)
    const [user, setUser] = useState<iUser>({} as iUser);

    useEffect(() => {
      getUser().then(userData => setUser(userData))
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
        id="TopBar"
        component={Container}
        width="100%"
        height={theme.spacing(mdDown ? 9 : 10)}
        bgcolor={theme.palette.background.paper}
        display='flex'
        justifyContent={smDown ? 'space-between' : 'end'}
        alignItems='center'
      >
        {smDown && (
          <Button
            color='primary'
            onClick={toggleDrawerOpen}
          >
            {isDrawerOpen ? <MenuOpenIcon /> : <MenuIcon />}
          </Button>
        )}

        <Box
          width={theme.spacing(28)}
          height={theme.spacing(9)}
          display='flex'
          flexDirection='row'
          gap={3}
          alignItems='center'
          marginRight={theme.spacing(2)}
        >
          
          <Button /*TODO onClick={toggleTheme}*/  >
            <Brightness6RoundedIcon color='primary' />
          </Button>
        
          <Box>
            {/*!smDown && (<Typography */}
            <Typography variant={mdDown ? 'body2' : 'subtitle1'}>
              {user.userName}
            </Typography>

            {/*
            <Typography variant={mdDown ? 'body2' : 'subtitle1'}>
              {user.role[0].toString()}
            </Typography>
            */}
          </Box>

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
                      {user.email}
                  </Typography>
                </Typography>
              </Box>

              <Divider />

              <Button onClick={handleLogout}>
                <LogoutIcon color='primary' />
                Sair
              </Button>
            </Box>
          </Popover>

        </Box>
      </Box>
    );

    const childrenContent = (
      <Box
        // component={Container}
        height='100%'
        display='flex'
        bgcolor={theme.palette.background.default}
        flexDirection='column'
        gap={theme.spacing(3)}
      >
        <Box
          display='flex'
          marginX={smDown ? theme.spacing(2) : mdDown ? theme.spacing(5) : theme.spacing(8)}
          padding={theme.spacing(2)}
          gap={theme.spacing(1)}
          maxHeight={theme.spacing(70)}
          overflow='auto'
          bgcolor={theme.palette.background.paper}
          component={Paper}
        >
          {children}
        </Box >

        <Box
          overflow='hidden'
          display='flex'
          flexDirection='column'
          gap={theme.spacing(3)}
        >
          <Footer />
        </Box>
      </Box >
    );

    return (
      <Box
        id="BasePageLayout" 
        sx={{ width: "100%" }}
        display='flex'
        flexDirection='column'
        gap={4}
      >
        {topBar}
        {childrenContent}
      </Box>
    )
}