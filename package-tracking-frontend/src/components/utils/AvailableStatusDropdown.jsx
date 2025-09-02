export default function AvailableStatusDropdown({ trackingNumber, availableStatusTransitions, onStatusChange }) {
  if (!availableStatusTransitions || availableStatusTransitions.length === 0) {
    return <span>-</span>
  }

  return (
    <select
      defaultValue=''
      onChange={(e) => {
        const newStatus = e.target.value
        if (newStatus) {
          onStatusChange(trackingNumber, newStatus)
          e.target.value = ''
        }
      }}
    >
      <option value='' disabled>
        Change Status
      </option>
      {availableStatusTransitions.map((status) => (
        <option key={status} value={status}>
          {status}
        </option>
      ))}
    </select>
  )
}