import React from "react";
import Information from "./component/Information";
import DashboardLayout from "../../layout/DashboardLayout";

const Profile = () => {
  return (
    <DashboardLayout>
      <div className="p-12 flex justify-center items-start gap-6 bg-slate-100">
        <div className="w-[720px]">
          <Information />
        </div>
      </div>
    </DashboardLayout>
  );
};

export default Profile;
