import React from 'react';
import {Typography, TableCell, Checkbox, Avatar, Box } from '@mui/material';

interface User {
  name: { first: string; last: string };
  email: string;
  phone: string;
  picture: { thumbnail: string };
  location: { street: { number: number; name: string }; city: string; state: string; postcode: string };
  dob: { date: string; age: number };
  registered: { date: string };
}

interface UserRowProps {
  user: User;
}

const UserRow: React.FC<UserRowProps> = ({ user }) => {
  return (
    <>
      <TableCell><Checkbox /></TableCell>
      <TableCell>
        <Box sx={{ display: 'flex', alignItems: 'center' }}>
          <Avatar src={user.picture.thumbnail} alt={`${user.name.first} ${user.name.last}`} sx={{ mr: 1, width: { xs: 24, sm: 40 }, height: { xs: 24, sm: 40 } }} />
          <Typography variant="body2" sx={{ fontSize: { xs: '0.75rem', sm: '0.875rem' } }}>
            {`${user.name.first} ${user.name.last}`}
          </Typography>
        </Box>
      </TableCell>
      <TableCell sx={{ fontSize: { xs: '0.75rem', sm: '0.875rem' } }}>{user.email}</TableCell>
      <TableCell sx={{ fontSize: { xs: '0.75rem', sm: '0.875rem' } }}>{user.phone}</TableCell>
    </>
  );
};

export default UserRow;